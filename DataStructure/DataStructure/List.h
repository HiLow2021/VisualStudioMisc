#pragma once
#include <stdlib.h>

template<typename T>
struct Node
{
	T Value;
	Node* Next;
};

// テンプレートを使う場合は実装をヘッダー内に書かないと、外部参照未解決というエラーが発生します。
// エラーを発生させない方法もありますが、一般的にはヘッダー内に実装も記述するようです。
template<typename T>
class List
{
public:
	List();
	~List();
	void Add(const T&);
	int Count();
	void Remove(const T&);
	void Show();

private:
	Node<T> _dummy;
	Node<T>* _Root = &_dummy;
	Node<T>* _Current = _Root;
	int _Size = 0;
};

template<typename T>
List<T>::List()
{
}

template<typename T>
List<T>::~List()
{
}

template<typename T>
void List<T>::Add(const T& Source)
{
	Node<T>* p = (Node<T>*)malloc(sizeof(Node<T>));

	if (p == nullptr) {
		throw "メモリが足りません。";
	}

	_Current->Next = p;
	_Current = _Current->Next;
	_Current->Value = Source;
	_Current->Next = nullptr;
	_Size++;
}

template<typename T>
int List<T>::Count()
{
	return _Size;
}

template<typename T>
void List<T>::Remove(const T& Source)
{
	Node<T>* prev = _Root;
	Node<T>* p = _Root->Next;

	while (p != nullptr)
	{
		if (p->Value == Source) {
			if (p->Next == nullptr) {
				prev->Next = nullptr;
				_Current = prev;
			}
			else {
				prev->Next = p->Next;
			}

			free(p);
			_Size--;
			break;
		}

		prev = p;
		p = p->Next;
	}
}

template<typename T>
void List<T>::Show()
{
	Node<T>* p = _Root->Next;

	while (p != nullptr)
	{
		printf_s("%d\n", p->Value);
		p = p->Next;
	}

	printf_s("\n");
}