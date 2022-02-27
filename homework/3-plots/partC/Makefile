Compiler = mcs
Runtime = mono
CSflags =-optimize+
Plot = gnuplot
IDIR = Include

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;


#I want make to update if i change the makefie, so I make this dependent on the makefile
all : gamma.png

#Also plots lngamma.png
gamma.png : gamma_approx.tsv plot.gpi
;gnuplot plot.gpi

%.tsv : %.exe
;$(Runtime) $< > $@

gamma_approx.exe: gamma_approx.cs Makefile cmath.dll
;$(Compiler) $< -reference:cmath.dll

cmath.dll : $(IDIR)/cmath.cs $(IDIR)/complex.cs
;$(Compiler) -target:library -out:./cmath.dll $^


.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f gamma_approx.tsv [Oo]ut* *.exe *.png [Ll]og*
# remove secondary files
