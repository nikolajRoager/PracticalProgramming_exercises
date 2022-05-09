.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : exp.pdf

exp.pdf : exp.tsv
;gnuplot plot.gpi

exp.tsv: exp.exe
;$(RUN) $< > exp.tsv


exp.exe: exp.cs
;$(CSC) -target:exe -out:$@ $<

.PHONY: clean

clean:
;rm -f *.dll *.exe log* *.png out*
