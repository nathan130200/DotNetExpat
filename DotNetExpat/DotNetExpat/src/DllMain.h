#include "expat.h"

#ifndef DLLMAIN_H
#define DLLMAIN_H

#ifndef HAVE_EXPAT_LIBRARY
#error Expat not found.
#endif

#ifdef DotNetExpat_EXPORTS
    #ifdef _WIN32
        #define DOTNETEXPAT_API __declspec(dllexport)
    #else
        #define DOTNETEXPAT_API __attribute__((visibility("default")))
    #endif
#else
    #ifdef _WIN32
        #define DOTNETEXPAT_API __declspec(dllimport)
    #else
        #define DOTNETEXPAT_API
    #endif
#endif // DotNetExpat_EXPORTS

#ifdef __cplusplus
extern "C"
{
#endif

DOTNETEXPAT_API XML_Parser Expat_Create(const XML_Char* encoding);
DOTNETEXPAT_API void Expat_Release(XML_Parser parser);
DOTNETEXPAT_API XML_Bool Expat_Reset(XML_Parser parser, const XML_Char* encoding);
DOTNETEXPAT_API XML_Status Expat_Parse(XML_Parser parser, char* buffer, int count, char end);
DOTNETEXPAT_API const XML_LChar* Expat_GetErrorDescription(XML_Error code);
DOTNETEXPAT_API XML_Error Expat_GetErrorCode(XML_Parser parser);
DOTNETEXPAT_API XML_Size Expat_GetCurrentLineNumber(XML_Parser parser);
DOTNETEXPAT_API XML_Index Expat_GetCurrentByteIndex(XML_Parser parser);
DOTNETEXPAT_API XML_Index Expat_GetCurrentByteCount(XML_Parser parser);
DOTNETEXPAT_API void Expat_SetUserData(XML_Parser parser, void* data);
DOTNETEXPAT_API void Expat_SetElementHandler(XML_Parser parser, XML_StartElementHandler start, XML_EndElementHandler end);

#ifdef __cplusplus
}
#endif

#endif // !DLLMAIN_H