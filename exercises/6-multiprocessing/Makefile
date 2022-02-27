Compiler = mcs
Runtime = mono
CSflags =-optimize+

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

#I want make to update if i change the makefie, so I make this dependent on the makefile
all : Out.txt
;cat $<

Out.txt : multiharm.exe
;\time -p $(Runtime) multiharm.exe 1e8 > $@


multiharm.exe : main.cs Makefile
;$(Compiler) $< -target:exe -out:$@

.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f [Oo]ut* *.exe
# remove secondary files
