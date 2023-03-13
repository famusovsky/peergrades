#ifndef PEERGRADE1CPP__ADJACENCYMATRIX_H_
#define PEERGRADE1CPP__ADJACENCYMATRIX_H_
#include "Graph.h"
#include <vector>

// Класс, представляющий Матрицу смежности и наследующийся от класса Граф.
class AdjacencyMatrix : virtual Graph {

  // Матрица, номера столбцов/строк которой означают номера вершин графа,
  // на пересечении строк и столбцов ставится 1, если вершины соединены ребром в графе, иначе - 0.
  std::vector<std::vector<int>> matrix;

 public:

  // Конструктор данного класса, получающий информацию из некоторого данного потока.
  explicit AdjacencyMatrix(std::istream &istream);
  
  // Функция, выполняющая печать Матрицы смежности в данный поток в виде Списка рёбер.
  void PrintAsEdgeList(std::ostream &ostream) override;
  
  // Функция, выполняющая печать Матрицы смежности в данный поток в виде Списка смежности.
  void PrintAsAdjacencyList(std::ostream &ostream) override;

  // Функция, выполняющая печать Матрицы смежности в данный поток в виде Матрицы смежности.
  void PrintAsAdjacencyMatrix(std::ostream &ostream) override;

  // Функция, выполняющая печать Матрицы смежности в данный поток в виде Списка инциденций.
  void PrintAsIncidenceMatrix(std::ostream &ostream) override;

  // Функция, выполняющая печать в данный поток списка (полу)степеней вершин Матрицы смежности.
  void DegreesCount(std::ostream &ostream) override;

  // Функция, выполняющая печать в данный поток количества рёбер Матрицы смежности.
  void EdgesCount(std::ostream &ostream) override;
};

#endif //PEERGRADE1CPP__ADJACENCYMATRIX_H_
