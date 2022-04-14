import numpy as np;
import scipy as sp;
from scipy import integrate

i=0

#By marking this variable as globel, we can see how many calls are made
def invx4(x):
    global i;
    i=i+1
    return 1/(x*x*x*x)

#By marking this variable as globel, we can see how many calls are made
def gauss(x):
    global i;
    i=i+1
    return np.exp(-x*x)

print("scipy 1/x^4 -> ",sp.integrate.quad(invx4, 1, np.inf)," i=",i)

i=0
print("scipy Exp(-x^2) -> ",sp.integrate.quad(gauss, -np.inf, np.inf)," i=",i)

