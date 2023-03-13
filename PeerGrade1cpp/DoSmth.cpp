#include "DoSmth.h"
#include <iostream>
#include <fstream>

void DoSmthWithAdjacencyList(AdjacencyList adjacency_list) {
  while (true) {
    std::cout
        << "������� \"1\" ��� ������ � ����, \'���� �����\' ��� ������ � �������; ����� ������� ����� ��������:\n"
        << "\"1\" - ������� ��� ������ ����, \"2\" - ������� ��� ������ ���������, \"3\" - ������� ��� ������� ����������, \"4\" - ������� ��� ������� ���������, \"5\" - ��������� ������� (����)��������, \"6\" - ��������� ������� ����� ����\n, \'���� �����\' - ��������� ������ � ������ ������\n";
    int inp_1, inp_2;
    std::cin >> inp_1 >> inp_2;
    std::ofstream fout("../out.txt");
    switch (inp_2) {
      case 1:adjacency_list.PrintAsEdgeList(inp_1 == 1 ? fout : std::cout);
        continue;
      case 2:adjacency_list.PrintAsAdjacencyList(inp_1 == 1 ? fout : std::cout);
        continue;
      case 3:adjacency_list.PrintAsIncidenceMatrix(inp_1 == 1 ? fout : std::cout);
        continue;
      case 4:adjacency_list.PrintAsAdjacencyMatrix(inp_1 == 1 ? fout : std::cout);
        continue;
      case 5:adjacency_list.DegreesCount(inp_1 == 1 ? fout : std::cout);
        continue;
      case 6:adjacency_list.EdgesCount(inp_1 == 1 ? fout : std::cout);
        continue;
      default:break;
    }
    break;
  }
}

void DoSmthWithAdjacencyMatrix(AdjacencyMatrix adjacency_matrix) {
  while (true) {
    std::cout
        << "������� \"1\" ��� ������ � ����, \'���� �����\' ��� ������ � �������; ����� ������� ����� ��������:\n"
        << "\"1\" - ������� ��� ������ ����, \"2\" - ������� ��� ������ ���������, \"3\" - ������� ��� ������� ����������, \"4\" - ������� ��� ������� ���������, \"5\" - ��������� ������� (����)��������, \"6\" - ��������� ������� ����� ����\n, \'���� �����\' - ��������� ������ � ������ ������\n";
    int inp_1, inp_2;
    std::cin >> inp_1 >> inp_2;
    std::ofstream fout("../out.txt");
    switch (inp_2) {
      case 1:adjacency_matrix.PrintAsEdgeList(inp_1 == 1 ? fout : std::cout);
        continue;
      case 2:adjacency_matrix.PrintAsAdjacencyList(inp_1 == 1 ? fout : std::cout);
        continue;
      case 3:adjacency_matrix.PrintAsIncidenceMatrix(inp_1 == 1 ? fout : std::cout);
        continue;
      case 4:adjacency_matrix.PrintAsAdjacencyMatrix(inp_1 == 1 ? fout : std::cout);
        continue;
      case 5:adjacency_matrix.DegreesCount(inp_1 == 1 ? fout : std::cout);
        continue;
      case 6:adjacency_matrix.EdgesCount(inp_1 == 1 ? fout : std::cout);
        continue;
      default:break;
    }
    break;
  }
}

void DoSmthWithIncidenceMatrix(IncidenceMatrix incidence_matrix) {
  while (true) {
    std::cout
        << "������� \"1\" ��� ������ � ����, \'���� �����\' ��� ������ � �������; ����� ������� ����� ��������:\n"
        << "\"1\" - ������� ��� ������ ����, \"2\" - ������� ��� ������ ���������, \"3\" - ������� ��� ������� ����������, \"4\" - ������� ��� ������� ���������, \"5\" - ��������� ������� (����)��������, \"6\" - ��������� ������� ����� ����\n, \'���� �����\' - ��������� ������ � ������ ������\n";
    int inp_1, inp_2;
    std::cin >> inp_1 >> inp_2;
    std::ofstream fout("../out.txt");
    switch (inp_2) {
      case 1:incidence_matrix.PrintAsEdgeList(inp_1 == 1 ? fout : std::cout);
        continue;
      case 2:incidence_matrix.PrintAsAdjacencyList(inp_1 == 1 ? fout : std::cout);
        continue;
      case 3:incidence_matrix.PrintAsIncidenceMatrix(inp_1 == 1 ? fout : std::cout);
        continue;
      case 4:incidence_matrix.PrintAsAdjacencyMatrix(inp_1 == 1 ? fout : std::cout);
        continue;
      case 5:incidence_matrix.DegreesCount(inp_1 == 1 ? fout : std::cout);
        continue;
      case 6:incidence_matrix.EdgesCount(inp_1 == 1 ? fout : std::cout);
        continue;
      default:break;
    }
    break;
  }
}

void DoSmthWithEdgeList(EdgeList edge_list) {
  while (true) {
    std::cout
        << "������� \"1\" ��� ������ � ����, \'���� �����\' ��� ������ � �������; ����� ������� ����� ��������:\n"
        << "\"1\" - ������� ��� ������ ����, \"2\" - ������� ��� ������ ���������, \"3\" - ������� ��� ������� ����������, \"4\" - ������� ��� ������� ���������, \"5\" - ��������� ������� (����)��������, \"6\" - ��������� ������� ����� ����\n, \'���� �����\' - ��������� ������ � ������ ������\n";
    int inp_1, inp_2;
    std::cin >> inp_1 >> inp_2;
    std::ofstream fout("../out.txt");
    switch (inp_2) {
      case 1:edge_list.PrintAsEdgeList(inp_1 == 1 ? fout : std::cout);
        continue;
      case 2:edge_list.PrintAsAdjacencyList(inp_1 == 1 ? fout : std::cout);
        continue;
      case 3:edge_list.PrintAsIncidenceMatrix(inp_1 == 1 ? fout : std::cout);
        continue;
      case 4:edge_list.PrintAsAdjacencyMatrix(inp_1 == 1 ? fout : std::cout);
        continue;
      case 5:edge_list.DegreesCount(inp_1 == 1 ? fout : std::cout);
        continue;
      case 6:edge_list.EdgesCount(inp_1 == 1 ? fout : std::cout);
        continue;
      default:break;
    }
    break;
  }
}
