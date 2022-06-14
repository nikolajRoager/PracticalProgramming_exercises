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

for xi in range(0,n):
    for yi in range(0,m):
        print(((long1-long0)*xi/(n-1)+long0),( (lat1-lat0)*yi/(n-1)+lat0),imarray[int(yi*(px_h-1)/(m-1))][int((xi*(px_w-1)/(n-1)))]-half_uint16_max)
