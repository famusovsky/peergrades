#include <iostream>
#include <string>
#include "IncidenceMatrix.h"
#include "AdjacencyList.h"
#include "AdjacencyMatrix.h"
#include "DoSmth.h"
#include "EdgeList.h"
#include <fstream>
#include <clocale>
int main() {
  setlocale(LC_ALL, "Russian");
  int inp;
  while (true) {
    std::cout << "\"1\" для ввода нового графа, \'Иное число\' для выхода из программы\n";
    std::cin >> inp;
    if (inp == 1) {
      int fromFile, graphType;
      std::cout
          << "Выберите вид вводимого графа: \"1\" - Список смежности, \"2\" - Матрица смежности, \"3\" - Матрица инциденций, \'Иное число\' - Список рёбер\n";
      std::cin >> graphType;
      std::cout
          << "Введите \"1\", если хотите совершить ввод из файла, иначе введите любое другое число\n";
      std::cin >> fromFile;
      if (fromFile != 1) {
        std::cout
            << "Если граф ориентированный введите \"1\", иначе введите другое число; затем начните ввод графа, для завершения ввода введите: \"End\".\n";
      }
      std::ifstream fin("../in.txt");
      switch (graphType) {
        case 1: {
          AdjacencyList newList(fromFile == 1 ? fin : (std::cin >> std::ws));
          DoSmthWithAdjacencyList(newList);
          break;
        }
        case 2: {
          AdjacencyMatrix newMatrix(fromFile == 1 ? fin : (std::cin >> std::ws));
          DoSmthWithAdjacencyMatrix(newMatrix);
          break;
        }
        case 3: {
          IncidenceMatrix newMatrix(fromFile == 1 ? fin : (std::cin >> std::ws));
          DoSmthWithIncidenceMatrix(newMatrix);
          break;
        }
        default: {
          EdgeList newList(fromFile == 1 ? fin : (std::cin >> std::ws));
          DoSmthWithEdgeList(newList);
          break;
        }
      }
    } else {
      break;
    }
  }
}
