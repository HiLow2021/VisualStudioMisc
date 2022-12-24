// main.cpp : アプリケーションのエントリ ポイントを定義します。
//

#include "stdafx.h"
#include <conio.h>
#include <ctype.h>
#include "List.h"

int main()
{
	int ch;
	int i = 1;
	List<int> list;

	while (true)
	{
		printf_s("キーを入力してください\n");
		ch = _getch();
		ch = toupper(ch);

		switch (ch)
		{
		case 'A':
			list.Add(i++);
			break;

		case 'C':
			printf_s("総数:%d\n",list.Count());
			break;

		case 'E':
			return 0;

		case 'R':
			printf_s("削除する数字を入力してください\n");
			scanf_s("%d", &ch);
			list.Remove(ch);
			break;

		case 'S':
			list.Show();
			break;
		}
	}
}

