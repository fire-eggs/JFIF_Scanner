# JFIF_Scanner
Quickly scan JPEG files and identify those with 'problematic' DPI settings.

The following image on the left (see Examples) will be displayed by "Windows Photo Viewer" as on the right:
![demo](./demo.png?raw=true)

Usage:

JFIF_Scanner <path containing JPG files>

The program will recursively scan the path and subfolders, examine each JPG file in turn, and print a message
to the console if an image is likely to appear "poorly" in the "Windows Photo Viewer".

There are two scenarios:
1. The image displays as a thin line, as in the image above.
2. The image displays stretched or distorted.
3. 
