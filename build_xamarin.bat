xcopy /s host\pkg HENkaku\HENkaku\Server\Handler\Resource\pkg
copy host\exploit.html HENkaku\HENkaku\Server\Handler\Resource\exploit.html
python3 preprocess.py loader.rop.bin HENkaku\HENkaku\Server\Handler\Resource\stage1.bin
python3 preprocess.py exploit.rop.bin HENkaku\HENkaku\Server\Handler\Resource\stage2.bin
convert -geometry 72x72 host\pkg\sce_sys\icon0.png HENkaku\HENkaku.Droid\Resources\drawable-hdpi\icon.png
convert -geometry 96x96 host\pkg\sce_sys\icon0.png HENkaku\HENkaku.Droid\Resources\drawable-xhdpi\icon.png
convert -geometry 144x144 host\pkg\sce_sys\icon0.png HENkaku\HENkaku.Droid\Resources\drawable-xxhdpi\icon.png
copy HENkaku\HENkaku.Droid\Resources\drawable-hdpi\icon.png HENkaku\HENkaku.Droid\Resources\drawable\icon.png
@echo "done."