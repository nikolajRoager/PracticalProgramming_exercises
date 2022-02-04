Compiler = mcs
CSflags =-optimize+ -platform:arm

.RECIPEPREFIX := ;
#I always had a hard time placing those literal tabs, this seems better

out.txt : hello.exe
;mono hello.exe > out.txt

hello.exe : hello.cs #Dependency, if hello.cs has changed this will be re-run
;$(Compiler) hello.cs -out:hello.exe $(CSflags)
# On Linux there is no strong standard for what to call executables, I often use .app because I used to use mac, but lets go with .exe

clean:# a phoney target, can be used to run any commands
;rm -f out.txt hello.exe
# remove secondary files
