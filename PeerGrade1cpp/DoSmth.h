#ifndef PEERGRADE1CPP__DOSMTH_H_
#define PEERGRADE1CPP__DOSMTH_H_
#include "AdjacencyList.h"
#include "AdjacencyMatrix.h"
#include "IncidenceMatrix.h"
#include "EdgeList.h"

// �������, ����������� ��������� ��������� �������� �������� �� ������� ���������.
void DoSmthWithAdjacencyList(AdjacencyList adjacency_list);

// �������, ����������� ��������� ��������� �������� �������� � �������� ���������.
void DoSmthWithAdjacencyMatrix(AdjacencyMatrix adjacency_matrix);

// �������, ����������� ��������� ��������� �������� �������� � �������� ����������.
void DoSmthWithIncidenceMatrix(IncidenceMatrix incidence_matrix);

// �������, ����������� ��������� ��������� �������� �������� �� ������� ����.
void DoSmthWithEdgeList(EdgeList edge_list);

#endif //PEERGRADE1CPP__DOSMTH_H_
