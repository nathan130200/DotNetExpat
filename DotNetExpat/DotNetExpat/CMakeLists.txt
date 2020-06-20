cmake_minimum_required (VERSION 3.8)

file(GLOB_RECURSE DOTNETEXPAT_SOURCES "src/*.cpp" "src/*.h")
find_package(EXPAT REQUIRED)

if(${EXPAT_FOUND})
    add_compile_definitions(HAVE_EXPAT_LIBRARY=1)
endif()

include_directories(${EXPAT_INCLUDE_DIR})
add_library (DotNetExpat SHARED ${DOTNETEXPAT_SOURCES} ${EXPAT_INCLUDE_DIR})
target_link_libraries(DotNetExpat PRIVATE ${EXPAT_LIBRARIES})