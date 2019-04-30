using System;
using System.Diagnostics;
using System.Collections.Generic;
using AudioSwitcher.AudioApi.CoreAudio;
using AudioSwitcher.AudioApi.CoreAudio.Interfaces;
using AudioSwitcher.AudioApi.CoreAudio.Threading;
using AudioSwitcher.AudioApi.Observables;
using AudioSwitcher.AudioApi.Session;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
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

        // Set flag for first serial port choice.
        public bool firstPortChoice = true;

        // Create a new thread to run in the background. See Form1_Load.
        Thread t;

        // Define playback device for system volume control.
        public CoreAudioDevice sysVol = new CoreAudioController().DefaultPlaybackDevice;

        public MixForm()
        {
            InitializeComponent();

            // Add the application combo boxes to the combo box list.
            combos.Add(comboBox1);
            combos.Add(comboBox2);
            combos.Add(comboBox3);

            // Set up the combo boxes such that each one can display each process.
            List<string> process_names = new List<string>();

            foreach (Process name in GetAudioProcesses())
            {
                process_names.Add(name.ProcessName);
            }

            foreach (string name in EnumerateApplications())
            {
                if (name != "")
                {
                    process_names.Add(name);
                }
            }

            foreach (ComboBox c in combos)
            {
                c.Items.AddRange(process_names.ToArray());
            }

            // Set up the port combo box.
            string[] ports = SerialPort.GetPortNames();
            comboBoxPorts.Items.AddRange(ports);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create and start a new thread in the load event.
            // Passing it the ReadArduino method to be run on the new thread.
            t = new Thread(ReadArduino);
            t.Start();
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

            while (true)
            {
                // Continuously check for updates once the port is selected and successfully opened.
                while (port.IsOpen)
                {
                    // Fetch incoming string from Arduino and spit up the values, which are separated by spaces.
                    string[] words = port.ReadLine().Split(' ');

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
                                    string app = comboText;
                                    SetApplicationVolume(app, val);
                                }
                            }

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

        // Stop button closes the port to stop the app.
        private void button1_Click(object sender, EventArgs e)
        {
            if (port.IsOpen)
            {
                port.Close();
            }
        }

        private void SearchBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //List<string> process_names1 = new List<string>
            //{
            //     Start with a list of common or likely selections.
            //    "All",
            //    "Chrome"
            //};

            //processlist = Process.GetProcessesByName(searchBox1.Text);

            //foreach (Process p in processlist)
            //{
            //    process_names1.Add(p.ProcessName);
            //}

            //comboBox1.Items.AddRange(process_names1.ToArray());

            //foreach (ComboBox c in combos)
            //{
            //    c.Items.AddRange(process_names1.ToArray());
            //}
        }


        // taken from https://stackoverflow.com/questions/52030208/c-sharp-get-list-of-audio-processes

        public static List<Process> GetAudioProcesses()
        {
            IMMDeviceEnumerator deviceEnumerator = null;
            IAudioSessionEnumerator sessionEnumerator = null;
            IAudioSessionManager2 mgr = null;
            IMMDevice speakers = null;
            List<Process> audioProcesses = new List<Process>();
            try
            {
                // get the speakers (1st render + multimedia) device
                deviceEnumerator = MMDeviceEnumeratorFactory.CreateInstance();
                deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out speakers);

                // activate the session manager. we need the enumerator
                Guid IID_IAudioSessionManager2 = typeof(IAudioSessionManager2).GUID;
                object o;
                speakers.Activate(ref IID_IAudioSessionManager2, 0, IntPtr.Zero, out o);
                mgr = (IAudioSessionManager2)o;

                // enumerate sessions for on this device
                mgr.GetSessionEnumerator(out sessionEnumerator);
                int count;
                sessionEnumerator.GetCount(out count);

                // search for an audio session with the required process-id
                for (int i = 0; i < count; ++i)
                {
                    IAudioSessionControl2 ctl = null;
                    try
                    {
                        sessionEnumerator.GetSession(i, out ctl);
                        ctl.GetProcessId(out int cpid);

                        audioProcesses.Add(Process.GetProcessById(cpid));
                    }
                    finally
                    {
                        if (ctl != null)
                        {
                            Marshal.ReleaseComObject(ctl);
                        }
                    }
                }

                return audioProcesses;
            }
            finally
            {
                if (sessionEnumerator != null) Marshal.ReleaseComObject(sessionEnumerator);
                if (mgr != null) Marshal.ReleaseComObject(mgr);
                if (speakers != null) Marshal.ReleaseComObject(speakers);
                if (deviceEnumerator != null) Marshal.ReleaseComObject(deviceEnumerator);
            }
        }


        // Copied from stack overflow gods, with minor changes to fix bugs for current context.

        public static float? GetApplicationVolume(string name)
        {
            ISimpleAudioVolume volume = GetVolumeObject(name);
            if (volume == null)
                return null;

            float level;
            volume.GetMasterVolume(out level);
            return level * 100;
        }

        public static bool? GetApplicationMute(string name)
        {
            ISimpleAudioVolume volume = GetVolumeObject(name);
            if (volume == null)
                return null;

            bool mute;
            volume.GetMute(out mute);
            return mute;
        }

        public static void SetApplicationVolume(string name, float level)
        {
            ISimpleAudioVolume volume = GetVolumeObject(name);
            if (volume == null)
                return;

            Guid guid = Guid.Empty;
            volume.SetMasterVolume(level / 100, ref guid);
        }

        public static void SetApplicationMute(string name, bool mute)
        {
            ISimpleAudioVolume volume = GetVolumeObject(name);
            if (volume == null)
                return;

            Guid guid = Guid.Empty;
            volume.SetMute(mute, ref guid);
        }

        public static IEnumerable<string> EnumerateApplications()
        {
            // get the speakers (1st render + multimedia) device
            IMMDeviceEnumerator deviceEnumerator = MMDeviceEnumeratorFactory.CreateInstance();
            IMMDevice speakers;
            deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out speakers);

            // activate the session manager. we need the enumerator
            Guid IID_IAudioSessionManager2 = typeof(IAudioSessionManager2).GUID;
            object o;
            speakers.Activate(ref IID_IAudioSessionManager2, 0, IntPtr.Zero, out o);
            IAudioSessionManager2 mgr = (IAudioSessionManager2)o;

            // enumerate sessions for on this device
            IAudioSessionEnumerator sessionEnumerator;
            mgr.GetSessionEnumerator(out sessionEnumerator);
            int count;
            sessionEnumerator.GetCount(out count);

            for (int i = 0; i < count; i++)
            {
                IAudioSessionControl ctl;
                sessionEnumerator.GetSession(i, out ctl);
                string dn;
                ctl.GetDisplayName(out dn);
                yield return dn;
                Marshal.ReleaseComObject(ctl);
            }
            Marshal.ReleaseComObject(sessionEnumerator);
            Marshal.ReleaseComObject(mgr);
            Marshal.ReleaseComObject(speakers);
            Marshal.ReleaseComObject(deviceEnumerator);
        }

        private static ISimpleAudioVolume GetVolumeObject(string name)
        {
            // get the speakers (1st render + multimedia) device
            IMMDeviceEnumerator deviceEnumerator = MMDeviceEnumeratorFactory.CreateInstance();
            IMMDevice speakers;
            deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out speakers);

            // activate the session manager. we need the enumerator
            Guid IID_IAudioSessionManager2 = typeof(IAudioSessionManager2).GUID;
            object o;
            speakers.Activate(ref IID_IAudioSessionManager2, 0, IntPtr.Zero, out o);
            IAudioSessionManager2 mgr = (IAudioSessionManager2)o;

            // enumerate sessions for on this device
            IAudioSessionEnumerator sessionEnumerator;
            mgr.GetSessionEnumerator(out sessionEnumerator);
            int count;
            sessionEnumerator.GetCount(out count);

            // search for an audio session with the required name
            // NOTE: we could also use the process id instead of the app name (with IAudioSessionControl2)
            ISimpleAudioVolume volumeControl = null;
            for (int i = 0; i < count; i++)
            {
                IAudioSessionControl ctl;
                sessionEnumerator.GetSession(i, out ctl);
                string dn;
                ctl.GetDisplayName(out dn);
                if (string.Compare(name, dn, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    volumeControl = ctl as ISimpleAudioVolume;
                    break;
                }
                Marshal.ReleaseComObject(ctl);
            }
            Marshal.ReleaseComObject(sessionEnumerator);
            Marshal.ReleaseComObject(mgr);
            Marshal.ReleaseComObject(speakers);
            Marshal.ReleaseComObject(deviceEnumerator);
            return volumeControl;
        }
    }

    public static class MMDeviceEnumeratorFactory
    {
        private static readonly Guid MMDeviceEnumerator = new Guid("BCDE0395-E52F-467C-8E3D-C4579291692E");

        internal static IMMDeviceEnumerator CreateInstance()
        {
            var type = Type.GetTypeFromCLSID(MMDeviceEnumerator);
            return (IMMDeviceEnumerator)Activator.CreateInstance(type);
        }
    }

    internal enum EDataFlow
    {
        eRender,
        eCapture,
        eAll,
        EDataFlow_enum_count
    }

    internal enum ERole
    {
        eConsole,
        eMultimedia,
        eCommunications,
        ERole_enum_count
    }

    [ComImport]
    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDeviceEnumerator
    {
        int NotImpl1();

        
        int GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice ppDevice);

        // the rest is not implemented
    }

    [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IMMDevice
    {
        
        int Activate(ref Guid iid, int dwClsCtx, IntPtr pActivationParams, [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);

        // the rest is not implemented
    }

    [Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAudioSessionManager2
    {
        int NotImpl1();
        int NotImpl2();

        
        int GetSessionEnumerator(out IAudioSessionEnumerator SessionEnum);

        // the rest is not implemented
    }

    [Guid("E2F5BB11-0570-40CA-ACDD-3AA01277DEE8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAudioSessionEnumerator
    {
        
        int GetCount(out int SessionCount);

        
        int GetSession(int SessionCount, out IAudioSessionControl Session);
    }

    [Guid("F4B1A599-7266-4319-A8CA-E70ACB11E8CD"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IAudioSessionControl
    {
        int NotImpl1();

        int GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] out string pRetVal);

        // the rest is not implemented
    }

    [Guid("87CE5498-68D6-44E5-9215-6DA47EF883D8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ISimpleAudioVolume
    {
        
        int SetMasterVolume(float fLevel, ref Guid EventContext);

        
        int GetMasterVolume(out float pfLevel);

        
        int SetMute(bool bMute, ref Guid EventContext);

        
        int GetMute(out bool pbMute);
    }
}

