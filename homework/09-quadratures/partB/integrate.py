import numpy as np;
import scipy as sp;
from scipy import integrate

i=0

#By marking this variable as globel, we can see how many calls are made
def invSqrtx(x):
    global i;
    i=i+1
    return 1/np.sqrt(x)

#By marking this variable as globel, we can see how many calls are made
def lnsqrtx(x):
    global i;
    i=i+1
    return np.log(x)/np.sqrt(x)

print("scipy 1/sqrt(x) -> ",sp.integrate.quadrature(invSqrtx, 0, 1, tol=0.001, rtol=0.001)," i=",i)

i=0

print("scipy ln(x)/sqrt(x) -> ",sp.integrate.quadrature(lnsqrtx , 0, 1, tol=0.001, rtol=0.001)," i=",i)
