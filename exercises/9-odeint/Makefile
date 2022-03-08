#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all: integrate.txt erf.png
;cat integrate.txt
erf.png: erf.gpi erf.txt
;pyxplot $<


integrate.txt erf.txt: integrate.exe bessel.py
;$(RUN) $< erf.txt bessel.txt > integrate.txt
;python3 bessel.py > bessel_py.txt

integrate.exe :main.cs matlib.dll
;$(CSC) -t:exe -out:$@ $(addprefix -r:,$(filter %.dll,$^)) $(filter %.cs,$^)


LIBDIR = ../../matlib
matlib.dll: \
$(LIBDIR)/integrate/integrate.cs \
$(LIBDIR)/odeint/ode.cs \
$(LIBDIR)/matrix/vector.cs
;$(CSC) -target:library -out:./$@ $^


clean:
;rm -f *.dll *.exe *.png *.txt log*
