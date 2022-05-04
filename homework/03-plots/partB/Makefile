Compiler = mcs
Runtime = mono
CSflags =-optimize+
Plot = gnuplot

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;


#I want make to update if i change the makefie, so I make this dependent on the makefile
all : gamma.png

#Also plots lngamma.png
gamma.png : gamma_approx.tsv lngamma_approx.tsv plot.gpi
;gnuplot plot.gpi

%.tsv : %.exe
;$(Runtime) $< > $@

%.exe: %.cs Makefile
;$(Compiler) $<

.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f gamma_approx.tsv [Oo]ut* *.exe *.png [Ll]og*
# remove secondary files
