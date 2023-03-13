#ifndef PEERGRADE1CPP__DOSMTH_H_
#define PEERGRADE1CPP__DOSMTH_H_
#include "AdjacencyList.h"
#include "AdjacencyMatrix.h"
#include "IncidenceMatrix.h"
#include "EdgeList.h"

// Функция, позволяющая выполнять требуемые заданием действия со Списком смежности.
void DoSmthWithAdjacencyList(AdjacencyList adjacency_list);

// Функция, позволяющая выполнять требуемые заданием действия с Матрицей смежности.
void DoSmthWithAdjacencyMatrix(AdjacencyMatrix adjacency_matrix);

// Функция, позволяющая выполнять требуемые заданием действия с Матрицей инциденций.
void DoSmthWithIncidenceMatrix(IncidenceMatrix incidence_matrix);

// Функция, позволяющая выполнять требуемые заданием действия со Списком рёбер.
void DoSmthWithEdgeList(EdgeList edge_list);

#endif //PEERGRADE1CPP__DOSMTH_H_
