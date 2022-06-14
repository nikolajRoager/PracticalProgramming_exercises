import numpy as np
from PIL import Image
import sys

#Microsoft broke the C# TIFF library on Linux and haven't fixed it, so Python it is

if (len(sys.argv)!=8):
    print("arguments: image_name n m long_0 lat_0 long_1 lat_1");
    exit();

n=int(sys.argv[2])
m=int(sys.argv[3])
long0=float(sys.argv[4])
lat0 =float(sys.argv[5])
long1=float(sys.argv[6])
lat1 =float(sys.argv[7])


Im = Image.open(sys.argv[1])


(px_w,px_h) =Im.size;


imarray = np.array(Im)

half_uint16_max=2**15;#The data type is mis-interpreted as uint16, so subtract half the max

#I assume the image has already been cropped to what we should convert

#We need to plot it in a format like this
# m  x0  x1  x2 ...
# y0 z00 z01 ..
# y1 z10 ...
# y2 ...
# ...

print(m, end ="\t")
for xi in range(0,m):
    print(((long1-long0)*xi/(m-1)+long0),end ="\t")
print();

for yi in range(0,n):
    print(( (lat1-lat0)*yi/(n-1)+lat0),end ="\t")
    for xi in range(0,m):
        print(imarray[int((n-1-yi)*(px_h-1)/(n-1))][int((xi*(px_w-1)/(m-1)))]-half_uint16_max,end ="\t")
    print();
