# CC13.2

A defalt SCS feature developed based on Clear Canvas 13.2 according to the following condition:
For some of the old devices(CT/DR), there might be no SCS in the images.
This will make the patitient information not shown correctly（messy code,especially for Chinese characters）in the image server.
This feature will allow the administrator to define a falut SCS for devices separately.
If an un-predefined device connects to the sever, the code will check if the SCS is defined in the image.
If no SCS defined, the system will be manually inserted for the device(currently, it is hardcoded).
But the administrator is able to change the default SCS for the device.

