import math
import scipy.integrate as integrate
ncalls=0
def f(x):
	global ncalls
	ncalls+=1
	return math.log(x)/math.sqrt(x)

result=integrate.quad(f,0,1)
print("result=",result," ncalls=",ncalls)
