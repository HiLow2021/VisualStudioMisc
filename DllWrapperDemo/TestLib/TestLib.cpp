// TestLib.cpp : スタティック ライブラリ用の関数を定義します。
//

#include "pch.h"
#include "framework.h"
#include "TestLib.h"
#include <string>

using namespace std;

DLL int CALL _AddNormal(int a, int b) {

	return a + b;
}

DLL _ResultAdd CALL _Add(int a, int b) {

	_ResultAdd result{ a, b, a + b };

	return result;
}

DLL _ResultSub CALL _Sub(int a, int b) {

	_ResultSub result{ a, b, a - b };

	return result;
}

DLL _GrandParent CALL _GetGrandParent(string familyName)
{
	auto grandParentName = familyName + " grandParent";
	auto grandParent = _GrandParent();
	auto parent = _GetParent(familyName);

	grandParent.name = grandParentName;
	grandParent.parent = parent;
	grandParent.child = parent.child;

	return grandParent;
}

DLL _Parent CALL _GetParent(string familyName)
{
	auto parentName = familyName + " parent";
	auto parent = _Parent();
	auto child = _GetChild(familyName);

	parent.name = parentName;
	parent.child = child;

	return parent;
}

DLL _Child CALL _GetChild(string familyName)
{
	auto childName = familyName + " child";
	auto child = _Child();

	child.name = childName;

	return child;
}
