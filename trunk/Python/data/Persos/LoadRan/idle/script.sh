for i in *.bmp
do
	convert $i -scale 50% -colors 256 $i.bmp
done