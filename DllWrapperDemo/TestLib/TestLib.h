#pragma once

#include <basetsd.h>
#include <string>

#define DLL __declspec(dllexport)
#define CALL __cdecl

using namespace std;

#if __cplusplus
extern "C" {
#endif

	typedef struct
	{
		UINT32 a;
		UINT32 b;
		UINT32 value;
	} _ResultAdd;

	typedef struct
	{
		UINT32 a;
		UINT32 b;
		UINT32 value;
	} _ResultSub;

	typedef enum
	{
		grandParent,
		parent,
		child,
	} _FamilyType;

	typedef struct
	{
		_FamilyType type = child;
		string name;
	} _Child;

	typedef struct
	{
		_FamilyType type = parent;
		string name;
		_Child child;
	} _Parent;

	typedef struct
	{
		_FamilyType type = grandParent;
		string name;
		_Parent parent;
		_Child child;
	} _GrandParent;

	DLL int CALL _AddNormal(int a, int b);
	DLL _ResultAdd CALL _Add(int a, int b);
	DLL _ResultSub CALL _Sub(int a, int b);
	DLL _GrandParent CALL _GetGrandParent(string familyName);
	DLL _Parent CALL _GetParent(string familyName);
	DLL _Child CALL _GetChild(string familyName);

#if __cplusplus // extern "C" {
}
#endif