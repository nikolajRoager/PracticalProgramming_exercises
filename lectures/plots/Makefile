Compiler = mcs
Runtime = mono
CSflags =-optimize+

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;


#I want make to update if i change the makefie, so I make this dependent on the makefile
all : erf.data.png

erf.gnuplot.png : erf.data.txt Makefile
;echo "\
;set terminal png\n\
;set output '$@' \n\
;set xlabel '$$ x $$ ' \n\
;set ylabel '$$ \mathrm{erf}(x) $$ ' \n\
;set key left top  \n\
;set title 'error function ' \n\
;plot '$<' index 0 using ($$ 1):(1-$$ 2) with points pointtype 1 title 'error function' " | tee log.gpi | gnuplot


erf.data.png : erf.data.txt Makefile
;echo "\
;set terminal png\n\
;set output '$@' \n\
;set xlabel '$$ x $$ ' \n\
;set ylabel '$$ \mathrm{erf}(x) $$ ' \n\
;set key left top  \n\
;set title 'error function ' \n\
;plot '$<' index 0 using ($$ 1):(1-$$ 2) with points pointtype 1 title 'error function' " | tee log.gpi | pyxplot

erf.data.txt : test.exe
;$(Runtime) $< > $@

%.exe: %.cs
;$(Compiler) $<

.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f [Oo]ut* *.exe *.png [Ll]og*
# remove secondary files
