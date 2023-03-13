cmake_minimum_required(VERSION 3.20)
project(PeerGrade1cpp)

set(CMAKE_CXX_STANDARD 20)

add_executable(PeerGrade1cpp main.cpp AdjacencyMatrix.cpp AdjacencyMatrix.h IncidenceMatrix.cpp IncidenceMatrix.h AdjacencyList.cpp AdjacencyList.h EdgeList.cpp EdgeList.h Graph.cpp Graph.h MatrixInput.cpp MatrixInput.h MaxFromSomeList.cpp MaxFromSomeList.h DoSmth.cpp DoSmth.h)
