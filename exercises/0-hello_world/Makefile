Compiler = mcs
CSflags =-optimize+

#I can't place literal tabs, tab default to space x4. This seems better
.RECIPEPREFIX := ;



#I want make to update if i change the makefie
out.txt : hello.exe Makefile
;mono hello.exe > out.txt

hello.exe : hello.cs  Makefile
;$(Compiler) hello.cs -out:hello.exe $(CSflags)
# On Linux there is no strong standard for what to call executables, I often use .app because I used to use mac, but lets go with .exe

.PHONY : clean
clean:# a phoney target, can be used to run any commands
;rm -f out.txt hello.exe
# remove secondary files
