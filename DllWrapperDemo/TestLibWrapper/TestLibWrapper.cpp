#include "pch.h"
#include "TestLibWrapper.h"
#include <string>
#include <msclr\marshal_cppstd.h>
#pragma comment(lib, "TestLib.lib")

using namespace std;
using namespace msclr::interop;
using namespace System;
using namespace System::Runtime::InteropServices;

namespace TestLibWrapper {
	int DllWrapper::AddNormal(int a, int b)
	{
		return _AddNormal(a, b);
	}

	ResultAdd^ DllWrapper::Add(int a, int b)
	{
		return gcnew ResultAdd(&_Add(a, b));
	}

	ResultSub^ DllWrapper::Sub(int a, int b)
	{
		return gcnew ResultSub(&_Sub(a, b));
	}

	GrandParent^ DllWrapper::GetGrandParent(String^ familyName)
	{
		GrandParent^ grandParent;
		Parent^ parent;
		Child^ child;
		auto ret = GetGrandParentInner(familyName, grandParent, parent, child);

		return ret;
	}

	Parent^ DllWrapper::GetParent(String^ familyName)
	{
		Parent^ parent;
		Child^ child;
		auto ret = GetParentInner(familyName, parent, child);

		return ret;
	}

	Child^ DllWrapper::GetChild(String^ familyName)
	{
		Child^ child;
		auto ret = GetChildInner(familyName, child);

		return ret;
	}

	String^ DllWrapper::GetString(String^ name)
	{
		auto c = "Hello World";
		auto d = string(c);
		auto e = _GetParent(d);
		auto f = gcnew Parent(&e);

		char arr[] = "World mine";
		String^ ret = marshal_as<String^>(arr);

		return ret;
	}

	cli::array<String^>^ DllWrapper::GetStringArray(int size)
	{
		auto ret = gcnew cli::array<String^>(size);

		for (size_t i = 0; i < size; i++)
		{
			ret[i] = i.ToString() + "”Ô–Ú";
		}

		return ret;
	}

	cli::array<float>^ DllWrapper::GetFloatArray(int size)
	{
		auto ret = gcnew cli::array<float>(size);

		for (size_t i = 0; i < size; i++)
		{
			ret[i] = i;
		}

		return ret;
	}

	GrandParent^ DllWrapper::GetGrandParentInner(String^ familyName, GrandParent^% grandParent, Parent^% parent, Child^% child)
	{
		marshal_context context;
		auto standardString = context.marshal_as<string>(familyName);
		auto _grandParent = _GetGrandParent(standardString);
		auto _grandParentPtr = new _GrandParent(_grandParent);

		grandParent = gcnew GrandParent(_grandParentPtr);
		parent = grandParent->Parent;
		child = grandParent->Child;

		return grandParent;
	}

	Parent^ DllWrapper::GetParentInner(String^ familyName, Parent^% parent, Child^% child)
	{
		marshal_context context;
		auto standardString = context.marshal_as<string>(familyName);
		auto _parent = _GetParent(standardString);
		auto _parentPtr = new _Parent(_parent);

		parent = gcnew Parent(_parentPtr);
		child = parent->Child;

		return parent;
	}

	Child^ DllWrapper::GetChildInner(String^ familyName, Child^% child)
	{
		marshal_context context;
		auto standardString = context.marshal_as<string>(familyName);
		auto _child = _GetChild(standardString);
		auto _childPtr = new _Child(_child);

		child = gcnew Child(_childPtr);

		return child;
	}
}