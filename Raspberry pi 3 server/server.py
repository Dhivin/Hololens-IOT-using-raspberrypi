import socket
import RPi.GPIO as GPIO
import time
UDP_IP = "172.16.4.181"
UDP_PORT = 8080

sock = socket.socket(socket.AF_INET, # Internet
                     socket.SOCK_DGRAM) # UDP
sock.bind((UDP_IP, UDP_PORT))
GPIO.setmode(GPIO.BCM)
GPIO.setup(4, GPIO.OUT)
GPIO.output(4,False)
while True:
    data, addr = sock.recvfrom(1024) # buffer size is 1024 bytes
    if data=="Lighton":
       GPIO.output(4,True)
    if data=="Lightoff":
       GPIO.output(4,False)
    print "received message:", data
    
