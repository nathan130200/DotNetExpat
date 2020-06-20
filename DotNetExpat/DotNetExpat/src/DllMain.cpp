#include "DllMain.h"

XML_Parser Expat_Create(const XML_Char* encoding = NULL) {
    return XML_ParserCreate(encoding);
};

void Expat_Release(XML_Parser parser) {
    XML_ParserFree(parser);
};

XML_Bool Expat_Reset(XML_Parser parser, const XML_Char* encoding = NULL) {
    return XML_ParserReset(parser, encoding);
}

XML_Status Expat_Parse(XML_Parser parser, char* buffer, int count, char end) {
    return XML_Parse(parser, buffer, count, (int)end);
};

const XML_LChar* Expat_GetErrorDescription(XML_Error code) {
    return XML_ErrorString(code);
};

XML_Error Expat_GetErrorCode(XML_Parser parser) {
    return XML_GetErrorCode(parser);
};

XML_Size Expat_GetCurrentLineNumber(XML_Parser parser) {
    return XML_GetCurrentLineNumber(parser);
};

XML_Index Expat_GetCurrentByteIndex(XML_Parser parser) {
    return XML_GetCurrentByteIndex(parser);
};

XML_Index Expat_GetCurrentByteCount(XML_Parser parser) {
    return XML_GetCurrentByteCount(parser);
};

void Expat_SetUserData(XML_Parser parser, void* data) {
    XML_SetUserData(parser, data);
};

void Expat_SetElementHandler(XML_Parser parser, XML_StartElementHandler start, XML_EndElementHandler end) {
    XML_SetElementHandler(parser, start, end);
};