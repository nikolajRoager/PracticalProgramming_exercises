Compiler = mcs
Runtime = mono
CSflags =-optimize+
IDIR = Include
#Just place include directory here, for now

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

#I want make to update if i change the makefie, so I make this dependent on the makefile
all : Out.txt log.txt Makefile#log.txt will be created by the same method which creates out.txt, there is no need for a specific rule.
;cat Out.txt
;cat log.txt

Out.txt : complex.exe Makefile
;$(Runtime) $< 1> $@ 2>log.txt

complex.exe : main.cs cmath.dll Makefile
;$(Compiler) main.cs -target:exe -reference:cmath.dll -out:$@

cmath.dll : $(IDIR)/cmath.cs $(IDIR)/complex.cs
;$(Compiler) -target:library -out:./cmath.dll $^ # note "./" in front of cmath.dll


.PHONY : clean
clean:
;rm -f *.txt *.exe
