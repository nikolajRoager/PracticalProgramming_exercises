#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : OutC.txt
;cat OutC.txt

OutC.txt log: partC.exe
;$(RUN) $< 7 1> OutC.txt 2>log
;python3 integrate.py >>  OutC.txt


partC.exe:main.cs quad.dll
;$(CSC) -target:exe -out:$@  -reference:quad.dll $<

quad.dll : quad.cs
;$(CSC) $< -target:library  -out:$@

.PHONY: clean

clean:
;rm -f *.dll *.exe log* *.png erf.tsv *.txt
