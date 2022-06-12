Compiler = mcs
Runtime = mono
CSflags =-optimize+
Plot = gnuplot

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;


all : gamma.png

#Also plots lngamma.png
gamma.png : gamma_approx.tsv lngamma_approx.tsv plot.gpi
;gnuplot plot.gpi


gamma_approx.tsv : gamma_approx.exe
;$(Runtime) $< > $@

gamma_approx.exe: gamma_approx.cs
;$(Compiler) $<

.PHONY : clean
clean:
;rm -f gamma_approx.tsv [Oo]ut* *.exe *.png [Ll]og*
# remove secondary files
