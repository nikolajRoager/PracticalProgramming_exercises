SDKDIR=$(HOME)/.dotnet/sdk/6.0.102# or whatever your sdk is
CSCPATH=$(SDKDIR)/Roslyn/bincore/csc.dll
NETSTANDARD=$(SDKDIR)/ref/netstandard.dll
CONFIG=$(SDKDIR)/vstest.console.runtimeconfig.json
DOTNET=DOTNET_CLI_TELEMETRY_OPTOUT=1; dotnet
CSC=$(DOTNET) $(CSCPATH) -reference:$(NETSTANDARD)
RUN=$(DOTNET) exec --runtimeconfig $(CONFIG)
# export DOTNET_CLI_TELEMETRY_OPTOUT=1
CSC = mcs
RUN = mono

all: Out.integ.txt Fig.erf.png Fig.oscillator.png
Fig.erf.png: fig.erf.gpi erf.data.txt
	pyxplot $<
Fig.oscillator.png: fig.oscillator.gpi oscillator.data.txt
	pyxplot $<

oscillator.data.txt: main-ode.exe
	$(RUN) $< 1> $@ 2>log

main-ode.exe: main-ode.cs matlib.dll

Out.integ.txt erf.data.txt:main-integ.exe test.py
	$(RUN) $< 1> Out.txt 2>erf.data.txt
	python3 test.py >> Out.txt

main-integ.exe:main-integ.cs matlib.dll

LIBDIR = ../../matlib
matlib.dll: \
	$(LIBDIR)/integrate/integrate.cs \
	$(LIBDIR)/odeint/ode.cs \
	$(LIBDIR)/matrix/vector.cs
	$(CSC) -target:library -out:./$@ $^
	
%.exe: %.cs
	$(CSC) -t:exe $(addprefix -r:,$(filter %.dll,$^)) $(filter %.cs,$^)

%.dll: %.cs
	$(CSC) -t:library -out:./%.dll $(addprefix -r:,$(filter %.dll,$^)) $(filter %.cs,$^)

clean:
	$(RM) *.dll *.exe *.png *.txt log*
