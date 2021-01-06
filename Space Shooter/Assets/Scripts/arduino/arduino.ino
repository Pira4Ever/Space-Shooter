#define pinBtn 5

void setup() {
pinMode(pinBtn, INPUT_PULLUP);
Serial.begin(9600);
}

void loop() {
  bool pinRead = digitalRead(pinBtn);
  if(pinRead == 1)
  {
    Serial.println(0);
  }
  else
  {
    Serial.println(1);
  }
}
