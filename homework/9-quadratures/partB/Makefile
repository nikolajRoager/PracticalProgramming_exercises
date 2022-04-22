#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

CSC = mcs
RUN = mono

all : outB.txt
;cat outB.txt

outB.txt log: partB.exe
;$(RUN) $< 7 1> outB.txt 2>log
;python3 integrate.py >> outB.txt


partB.exe:main.cs quad.dll
;$(CSC) -target:exe -out:$@  -reference:quad.dll $<

quad.dll : quad.cs
;$(CSC) $< -target:library  -out:$@

.PHONY: clean

clean:
;rm -f *.dll *.exe log* *.png erf.tsv
