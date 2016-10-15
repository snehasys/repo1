
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.1A\Bin\x64\makecert" -ic KmipCA.cer -iv KmipCA.pvk -n "CN=KMIPServer" -sv  KmipCA.pvk -pe -sky exchange KmipCA.cer

"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.1A\Bin\x64\cert2spc" KmipCA.cer KmipCA.spc

pvkimprt -pfx KmipCA.spc KmipCA.pvk