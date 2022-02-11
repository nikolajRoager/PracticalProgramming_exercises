Compiler = mcs
CSflags =-optimize+

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;


#I want make to update if i change the makefie, so I make this dependent on the makefile
out.txt : classes.exe Makefile
;mono classes.exe > out.txt

classes.exe : main.cs  Makefile
;$(Compiler) main.cs -out:classes.exe $(CSflags)
# On Linux there is no strong standard for what to call executables, I often use .app because I used to use mac, but lets go with .exe

.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f out.txt classes.exe
# remove secondary files
