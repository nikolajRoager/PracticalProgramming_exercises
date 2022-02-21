Compiler = mcs
Runtime = mono
CSflags =-optimize+
OUTNAME = table_demonstration.exe

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

all : Out.txt
;cat Out.txt

Out.txt : $(OUTNAME)
;$(Runtime) $< > $@

$(OUTNAME) : main.cs table.dll
;$(Compiler) main.cs -target:exe -reference:table.dll -out:$@

#I want make to update if i change the makefie, so I make this dependent on the makefile
table.dll :  table.cs Makefile
;$(Compiler) table.cs -target:library -out:$@


.PHONY : clean
clean:
;rm -f *.txt *.exe
