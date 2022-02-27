Compiler = mcs
Runtime = mono
CSflags =-optimize+

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;

#I want make to update if i change the makefie, so I make this dependent on the makefile
all : Out.txt
;cat $<

Out.txt : t4.exe t2.exe t1.exe
; cat /dev/null >$@
;time -p $(Runtime) t2.exe 1e8 >> $@
;time -p $(Runtime) t1.exe 1e8 >> $@
;time -p $(Runtime) t4.exe 1e8 >> $@


t4.exe : t4.cs Makefile
;$(Compiler) $< -target:exe -out:$@

t2.exe : t2.cs Makefile
;$(Compiler) $< -target:exe -out:$@

t1.exe : t1.cs Makefile
;$(Compiler) $< -target:exe -out:$@

.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f [Oo]ut* *.exe
# remove secondary files
