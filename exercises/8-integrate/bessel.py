import math
import scipy.special as sps
import numpy as np

for z in np.linspace(-3,3,24):
    print(z,sps.jv(1,z))
