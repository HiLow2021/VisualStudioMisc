#pragma once

#include <msclr\marshal_cppstd.h>
#include "TestLib.h"

using namespace cli;
using namespace msclr::interop;
using namespace System;

namespace TestLibWrapper {

	public enum class FamilyType
	{
		grandParent = 0,
		parent = 1,
		child = 2,
	};

	public ref class ResultAdd
	{
	internal:
		bool isResponsible;
		_ResultAdd* ptr;
		ResultAdd(_ResultAdd* ptr) : isResponsible{ false }, ptr{ ptr } {}

	public:
		ResultAdd() : isResponsible{ true }, ptr{ new _ResultAdd() } {}
		~ResultAdd() { this->!ResultAdd(); }
		!ResultAdd()
		{
			if (isResponsible && ptr != nullptr)
			{
				delete ptr;
				ptr = nullptr;
			}
		}

		property int A
		{
			int get() { return this->ptr->a; }
			void set(int value) { this->ptr->a = value; }
		}

		property int B
		{
			int get() { return this->ptr->b; }
			void set(int value) { this->ptr->b = value; }
		}

		property int Value
		{
			int get() { return this->ptr->value; }
			void set(int value) { this->ptr->value = value; }
		}

	};

	public ref class ResultSub
	{
	internal:
		bool isResponsible;
		_ResultSub* ptr;
		ResultSub(_ResultSub* ptr) : isResponsible{ false }, ptr{ ptr } {}

	public:
		ResultSub() : isResponsible{ true }, ptr{ new _ResultSub() } {}
		~ResultSub() { this->!ResultSub(); }
		!ResultSub()
		{
			if (isResponsible && ptr != nullptr)
			{
				delete ptr;
				ptr = nullptr;
			}
		}

		property int A
		{
			int get() { return this->ptr->a; }
			void set(int value) { this->ptr->a = value; }
		}

		property int B
		{
			int get() { return this->ptr->b; }
			void set(int value) { this->ptr->b = value; }
		}

		property int Value
		{
			int get() { return this->ptr->value; }
			void set(int value) { this->ptr->value = value; }
		}

	};

	public ref class Child
	{
	internal:
		bool isResponsible;
		_Child* ptr;
		Child(_Child* ptr) : isResponsible{ false }, ptr{ ptr } {}

	public:
		Child() : isResponsible{ true }, ptr{ new _Child() } {}
		~Child() { this->!Child(); }
		!Child()
		{
			if (isResponsible && ptr != nullptr)
			{
				delete ptr;
				ptr = nullptr;
			}
		}

		property FamilyType Type
		{
			FamilyType get() { return static_cast<FamilyType>(this->ptr->type); }
			void set(FamilyType value) { this->ptr->type = (_FamilyType)value; }
		}

		property String^ Name
		{
			String^ get() { return gcnew String(this->ptr->name.c_str()); }
			void set(String^ value) { this->ptr->name = marshal_as<string>(value); }
		}

	};

	public ref class Parent
	{
	internal:
		bool isResponsible;
		_Parent* ptr;
		Parent(_Parent* ptr) : isResponsible{ false }, ptr{ ptr } {}

	public:
		Parent() : isResponsible{ true }, ptr{ new _Parent() } {}
		~Parent() { this->!Parent(); }
		!Parent()
		{
			if (isResponsible && ptr != nullptr)
			{
				delete ptr;
				ptr = nullptr;
			}
		}

		property FamilyType Type
		{
			FamilyType get() { return static_cast<FamilyType>(this->ptr->type); }
			void set(FamilyType value) { this->ptr->type = (_FamilyType)value; }
		}

		property String^ Name
		{
			String^ get() { return gcnew String(this->ptr->name.c_str()); }
			void set(String^ value) { this->ptr->name = marshal_as<string>(value); }
		}

		property Child^ Child
		{
			TestLibWrapper::Child^ get() { return gcnew TestLibWrapper::Child(&this->ptr->child); }
			void set(TestLibWrapper::Child^ value) { this->ptr->child = *value->ptr; }
		}

	};

	public ref class GrandParent
	{
	internal:
		bool isResponsible;
		_GrandParent* ptr;
		GrandParent(_GrandParent* ptr) : isResponsible{ false }, ptr{ ptr } {}

	public:
		GrandParent() : isResponsible{ true }, ptr{ new _GrandParent() } {}
		~GrandParent() { this->!GrandParent(); }
		!GrandParent()
		{
			if (isResponsible && ptr != nullptr)
			{
				delete ptr;
				ptr = nullptr;
			}
		}

		property FamilyType Type
		{
			FamilyType get() { return static_cast<FamilyType>(this->ptr->type); }
			void set(FamilyType value) { this->ptr->type = (_FamilyType)value; }
		}

		property String^ Name
		{
			String^ get() { return gcnew String(this->ptr->name.c_str()); }
			void set(String^ value) { this->ptr->name = marshal_as<string>(value); }
		}

		property Parent^ Parent
		{
			TestLibWrapper::Parent^ get() { return gcnew TestLibWrapper::Parent(&this->ptr->parent); }
			void set(TestLibWrapper::Parent^ value) { this->ptr->parent = *value->ptr; }
		}

		property Child^ Child
		{
			TestLibWrapper::Child^ get() { return gcnew TestLibWrapper::Child(&this->ptr->child); }
			void set(TestLibWrapper::Child^ value) { this->ptr->child = *value->ptr; }
		}

	};

	public ref class DllWrapper
	{
	private:
		GrandParent^ GetGrandParentInner(String^ familyName, GrandParent^% grandParent, Parent^% parent, Child^% child);
		Parent^ GetParentInner(String^ familyName, Parent^% parent, Child^% child);
		Child^ GetChildInner(String^ familyName, Child^% child);

	public:
		int AddNormal(int a, int b);
		ResultAdd^ Add(int a, int b);
		ResultSub^ Sub(int a, int b);
		GrandParent^ GetGrandParent(String^ familyName);
		Parent^ GetParent(String^ familyName);
		Child^ GetChild(String^ familyName);
		String^ GetString(String^ name);
		cli::array<String^>^ GetStringArray(int size);
		cli::array<float>^ GetFloatArray(int size);
	};
}