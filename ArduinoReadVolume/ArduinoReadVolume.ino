/*

Written by Devon Bray - www.esologic.com
Use with permission only
9/21/2016

Edited by Andrew Chase Rosen
4/21/2019

*/

#define NUMAPPS 3
#define NUMPINS 4
#define NUMSMOOTHS 100

int input_pins[4] = {0, 1, 2, 3};

 
void setup() 
{
  Serial.begin(9600);
}

void loop() {

  // Initialize variables.
  int smoothed_val;
  int target_pin;
  long int running_average;
  
  for (int index = 0 ; index < NUMPINS; index++)
  {
    target_pin = input_pins[index];
    running_average = 0;
    for (int smoothing_index = 0; smoothing_index < NUMSMOOTHS; smoothing_index++)
    {
      running_average += analogRead(target_pin);
    }
    
    smoothed_val = (int)(running_average / NUMSMOOTHS); 

    Serial.print(smoothed_val);
    
    if (index < NUMPINS - 1)
    {
      Serial.print(" ");
    }
    else
    {
      Serial.print("\n");
    }
  }
}
