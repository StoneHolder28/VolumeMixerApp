using System;
using System.Diagnostics;
using System.Collections.Generic;
using AudioSwitcher.AudioApi.CoreAudio;
using AudioSwitcher.AudioApi.Session;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace VolumeMixerApp
{
    public partial class MixForm : Form
    {
        // Initialize serial port variable for the Arduino.
        public static SerialPort port = new SerialPort();

        // Create a list to populate with the number of application combo boxes. This will help optimize later code.
        List<ComboBox> combos = new List<ComboBox>();
        List<IAudioSession> session_list = new List<IAudioSession>();

        // Set flag for first serial port choice.
        public bool firstPortChoice = true;

        // Create a new thread to run in the background. See Form1_Load.
        Thread t;

        // Define playback device for system volume control.
        public CoreAudioDevice sysVol = new CoreAudioController().DefaultPlaybackDevice;

        public MixForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create and start a new thread in the load event.
            // Passing it the ReadArduino method to be run on the new thread.
            t = new Thread(ReadArduino);
            t.IsBackground = true;
            t.Start();

            // Add the application combo boxes to the combo box list.
            combos.Add(comboBox1);
            combos.Add(comboBox2);
            combos.Add(comboBox3);

            // Set up the combo boxes such that each one can display each process.
            updateSessions();
        }

        public void updateSessions()
        {
            port.Close();

            // Update the combo boxes and session list.
            List<string> process_names = new List<string>();
            process_names.Add("");

            foreach (IAudioSession session in GetAudioProcesses())
            {
                session_list.Add(session);
                Process session_process = Process.GetProcessById(session.ProcessId);
                process_names.Add(session_process.ProcessName);
            }

            foreach (ComboBox c in combos)
            {
                c.Items.Clear();
                c.Items.AddRange(process_names.ToArray());
            }

            // Update the port combo box.
            string[] ports = SerialPort.GetPortNames();
            comboBoxPorts.Items.Clear();
            comboBoxPorts.Items.AddRange(ports);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            updateSessions();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void ReadArduino()
        {
            // Initialize reading value and incoming string from Arduino.
            int reading = 0;
            List<int> vals = new List<int>();
            List<int> PreviousVals = new List<int>();
            for (int count = 0; count < combos.Count + 1; count++)
            {
                PreviousVals.Add(0);
            }

            // Continuously check for updates once the port is selected and successfully opened.
            while (true)
            {
                while (port.IsOpen)
                {
                    // Fetch incoming string from Arduino and spit up the values, which are separated by spaces.
                    string[] words = null;
                    try
                    {
                        words = port.ReadLine().Split(' ');
                    }
                    catch
                    {
                        break;
                    }

                    // Interpolate each value to a scale of 0 to 100.
                    vals.Clear();
                    foreach (string word in words)
                    {
                        // The Int32.Parse function converts the values to integers.
                        reading = map(Int32.Parse(word), 0, 1023, 0, 100);
                        vals.Add(reading);
                    }

                    // Prevent index-related exceptions.
                    while (vals.Count < combos.Count + 1)
                    {
                        vals.Add(0);
                    }

                    int val;
                    for (int index = 0; index < combos.Count + 1; index++)
                    {
                        val = vals[index];
                        if (val != PreviousVals[index])
                        {
                            // Check if the changed value is the system volume.
                            if (index == 0)
                            {
                                sysVol.Volume = val;
                            }

                            else
                            {
                                // Check if the changed value corresponds to an application.
                                string comboText = "";
                                combos[index - 1].Invoke(new MethodInvoker(delegate { comboText = combos[index - 1].Text; }));
                                if (comboText != string.Empty)
                                {
                                    IAudioSession app = session_list[index - 1];
                                    app.Volume = val;
                                }
                            }
                            // Preserve values for comparison.
                            PreviousVals[index] = val;
                        }
                    }
                }
            }
        }

        private void comboBoxPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If a port has been opened BY THIS APPLICATION, it will be closed before opening the next port.
            if (firstPortChoice == false)
            {
                port.Close();
            }
            // If this is the first time a port has been chosen, flip the flag.
            if (firstPortChoice == true)
            {
                port.BaudRate = 9600;
                port.DtrEnable = true;
                firstPortChoice = false;
            }

            // Open the port selected.
            port.PortName = comboBoxPorts.Text;
            port.Open();
        }

        // Define an interpolation function to map read values to a scale of 0 to 100.
        int map(int x, int in_min, int in_max, int out_min, int out_max)
        {
            return (x - in_min) * (out_max - out_min + 1) / (in_max - in_min + 1) + out_min;
        }

        // Stop button closes the port to stop volume control.
        private void button1_Click(object sender, EventArgs e)
        {
            if (port.IsOpen)
            {
                port.Close();
            }
        }

        // Collect audio sessions for volume control and process identificaiton.
        public IEnumerable<IAudioSession> GetAudioProcesses()
        {
            CoreAudioDevice ctrl = new CoreAudioController().DefaultPlaybackDevice;
            return ctrl.SessionController.ActiveSessions();
        }
    }
}

