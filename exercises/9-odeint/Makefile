#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all: pendulum.png log
;cat log
pendulum.png: plot.gpi data.tsv
;pyxplot $<


data.tsv: odeint.exe
;$(RUN) $< 1> $@ 2>log

odeint.exe :main.cs matlib.dll
;$(CSC) -t:exe -out:$@ $(addprefix -r:,$(filter %.dll,$^)) $(filter %.cs,$^)

LIBDIR = ../../matlib
matlib.dll: \
$(LIBDIR)/integrate/integrate.cs \
$(LIBDIR)/odeint/ode.cs \
$(LIBDIR)/matrix/vector.cs
;$(CSC) -target:library -out:./$@ $^

clean:
;rm -f *.dll *.exe *.png *.txt log*
