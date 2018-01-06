rm ./dist -Force -Recurse

ng build --prod

winscp /script=winscp-script.txt