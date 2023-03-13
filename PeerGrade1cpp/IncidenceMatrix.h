#ifndef PEERGRADE1CPP__INCIDENCEMATRIX_H_
#define PEERGRADE1CPP__INCIDENCEMATRIX_H_
#include "Graph.h"
#include <vector>

// Класс, представляющий Матрицу инциденций и наследующийся от класса Граф.
class IncidenceMatrix : virtual Graph {

  // Матрица, номера строк которой означают номера вершин графа, а номера столбцов - номера рёбер;
  // на пересечении строк и столбцов ставится 1, если ребро и вершина инцидентны, иначе - 0.
  std::vector<std::vector<int>> matrix;

 public:

  // Конструктор данного класса, получающий информацию из некоторого данного потока.
  explicit IncidenceMatrix(std::istream &istream);

  // Функция, выполняющая печать Матрицы инциденций в данный поток в виде Списка рёбер.
  void PrintAsEdgeList(std::ostream &ostream) override;

  // Функция, выполняющая печать Матрицы инциденций в данный поток в виде Списка смежности.
  void PrintAsAdjacencyList(std::ostream &ostream) override;

  // Функция, выполняющая печать Матрицы инциденций в данный поток в виде Матрицы смежности.
  void PrintAsAdjacencyMatrix(std::ostream &ostream) override;

  // Функция, выполняющая печать Матрицы инциденций в данный поток в виде Списка инциденций.
  void PrintAsIncidenceMatrix(std::ostream &ostream) override;

  // Функция, выполняющая печать в данный поток списка (полу)степеней вершин Матрицы инциденций.
  void DegreesCount(std::ostream &ostream) override;

  // Функция, выполняющая печать в данный поток количества рёбер Матрицы инциденций.
  void EdgesCount(std::ostream &ostream) override;
};

#endif //PEERGRADE1CPP__INCIDENCEMATRIX_H_
