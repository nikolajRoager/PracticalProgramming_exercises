.RECIPEPREFIX := ;

RUN = python3

all: OlympusMons_data.tsv VallesMarineris_data.tsv

OlympusMons_data.tsv : make_rectgrid.py OlympusMons.tiff
;$(RUN) $< OlympusMons.tiff 8 8 40 65 53 78 1> $@

VallesMarineris_data.tsv : make_rectgrid.py VallesMarineris.tiff
;$(RUN) $< VallesMarineris.tiff 16 48 70 90 130 110 1> $@

.PHONY: clean

clean:
;rm -f *.tsv
