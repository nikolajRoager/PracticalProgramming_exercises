SDKDIR=$(HOME)/.dotnet/sdk/6.0.102# or whatever your sdk is
CSCPATH=$(SDKDIR)/Roslyn/bincore/csc.dll
NETSTANDARD=$(SDKDIR)/ref/netstandard.dll
CONFIG=$(SDKDIR)/vstest.console.runtimeconfig.json
DOTNET=dotnet
CSC=$(DOTNET) $(CSCPATH) -reference:$(NETSTANDARD)
RUN=$(DOTNET) exec --runtimeconfig $(CONFIG)
# remember to export DOTNET_CLI_TELEMETRY_OPTOUT=1
CSC = mcs -optimize+ -platform:arm
RUN = mono -O=all #-O=all,-shared #--optimize=unsafe,loop,inline #--gc=sgen --llvm --optimize=all

CFLAGS = -Ofast -march=native
LDLIBS = $(shell gsl-config --libs)

comma:=,
empty:=
space:=$(empty) $(empty)
commalist = $(subst $(space),$(comma),$(1))

all: out.txt

out.txt: main.exe Makefile
	$(RUN) $< 7 | tee $@

main:main.c


out.times.txt: main.exe
	cat /dev/null > $@
	for N in `seq 73 5 133`; do \
	echo "N=$$N";\time -ao $@ -f "$$N %U" $(RUN) main.exe $$N ; \
	done

main.exe: main.cs matrix.dll
	$(CSC) $< -out:$@ -r:$(call commalist,$(filter-out $<,$^))

matrix.dll: jacobi.cs ../matrix/matrix.cs ../matrix/vector.cs
	$(CSC) $^ /t:library /out:$$(pwd)/$@

clean: ; $(RM) *.dll *.exe out.* *.log *.svg

#%.dll: %.cs ; $(CSC) -t:library $*.cs -o:$*.dll *log

test:
	echo $(LDLIBS)
	echo $$($(RUN) --version)
