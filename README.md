Esse repositório contém o projeto realizado pelo GER Drone para gerar dados sintéticos para treinar modelos de machine learning para o Desafio de Robótica Petrobrás 2021. 

Os dados gerados podem ser obtidos em [Dados brutos](https://drive.google.com/drive/folders/1wAd4orS4DzqpG2xD3Jn6hh7_kzwSqeea?usp=sharing), [Dados formatados para a YoloV5](https://universe.roboflow.com/ger/ger-drone-cbr-2021).

- simulation: contém a simulação criada na Unity, e os assets utilizados
- testes-exemplos: notebook executando um tutorial do TF/Keras
- exploration: notebooks explorando os dados gerados.
- datasets: dados de exemplo gerados na simulação anterior.  
- Mostrador: código utilizado para gerar as texturas dos mostradores digitais.


# Procedimentos realizados - ML e Dados sintéticos com a Unity

Nessa seção, iremos descrever como utilizamos a Unity para gerar dados sintéticos, e utilizamos esses dados para treinar um modelo da Yolov5 e um modelo customizado para detectar os mostradores digitais.

Temos também o [vídeo apresentado no Desafio de Inovação de Robótica Petrobrás da CBR 2021](https://youtu.be/_ptGLwz0t6o) que descreve o processo, (em especial, descreve as dificuldades e performance dos modelos, que não é descrito neste texto).

> **Glossário de conceitos e programas**
> - Unity: uma game engine, programa utilizado para criar jogos. Porém, é posível utilizá-los como simuladores para aplicações genérias, como arquitetura ou robótica.
> - Geração de dados sintéticos: técnica utilizada para gerar dados artificiais para treinar modelos de machine learning. Permite gerar dados com menor custo, porém que podem não representar corretamente a realidade. 
> - Yolov5: um modelo de inteligência artificial para detectar objetos em imagens, detectando bounding boxes.
> - Bounding box: tipo de label, caixa que indica a região em que um objeto está em uma imagem.
> - Label: informação sobre uma imagem, indica o que o modelo deve detectar (ex. classe e bounding box de objetos)
> - Key point: tipo de label, ponto de interesse na imagem que deve ser detectado.
> - Blender: programa de modelagem 3D.

## Geração de dados sintéticos

Utilizamos a Unity Engine para gerar os dados. Ela possui o pacote ["Unity Perception"](https://github.com/Unity-Technologies/com.unity.perception), que permite randomizar uma cena e salvar imagens com labels. 

É possível baixar a versão de estudante da Unity [aqui](https://unity.com/pt/products/unity-student), e  no repositório do pacote existe uma descrição de como [instalá-lo](https://github.com/Unity-Technologies/com.unity.perception/blob/main/com.unity.perception/Documentation~/SetupSteps.md).


O repositório [ger_drone_ai](https://github.com/ger-unicamp/ger_drone_ai) contém projeto da Unity criado.


> **Glossário rápido de Unity**:
> - Cena: um conjunto de objetos, semelhante a uma fase de um jogo.
> - Objeto: algum objeto existente na cena. Semelhante a ideia de um objeto de POO.
> - Componente: descreve um comportamento de um objeto. Existem alguns padrões (Transform, RigidBody, MeshRenderer, Light), e podem ser definidos por classes que herdam de MonoBehaviour (perceba como, idealmente, cada componente deve descrever um e apenas um comportamento, para aumentar a modularidade). Semelhante ao padrão de projeto [Component](https://gameprogrammingpatterns.com/component.html).
> - Asset: um elemento de um jogo, como modelos 3D, scripts ou sons.

O primeiro passo para gerar os dados é preparar uma cena, em que utilizamos os modelos disponibilizados pela competição. Após, é preciso definir randomizadores e labels, para randomizar a cena e gerar dados.

[Tutorais de como utilizar o pacote para gerar os dados após criar a cena.](https://github.com/Unity-Technologies/com.unity.perception)

### Modelos

Os modelos foram importados do repositório da competição, [Drone_Trial_League](https://github.com/LASER-Robotics/Drone_Trial_League/). Como os modelos estão na extensão utilizada pelo Gazebo, tivemos que converte-los para uma extensão utilizável pela Unity (ela importa o modelo direto, porém sem as texturas).

Criamos então um projeto no Blender como todos os objetos, e abrimos esse projeto na Unity. Como a Unity precisa que o Blender esteja instalado para carregar corretamente as texturas salvas por ele, separamos cada objeto do arquivo .blend para prefabs da Unity (foi preciso também reassociar cada material a cada objeto).

O único modelo não importado foi a água, que utilizamos um asset genérico da asset store.

### Randomizadores

Os randomizadores são utilizados pela Unity para randomizar propriedades dos objetos a cada iteração. Eles vem em conjuntos de componentes tags e randomizadores. Cada tag é associada a um objeto que será randomizado, e randomiza uma propriedade dela, enquanto o randomizador é associado ao cenário.

Criams as seguintes tags e randomizadores:

Tag|Descrição|Randomizador|Objetos
-|-|-|-
AnguloTag|Altera o ângulo espacial dos objetos|AnguloRandomizer|Objetos, Câmera, Luz.
CameraTag|Altera o campo de visão da câmera|CameraRandomizer|Câmera.
LuzTag|Altera a intensidade e coloração da luz|LuzRandomizer|Luz
MostradorTag|Altera os dígitos do mostrador|MostradorRandomizer|Mostradores digitais 
MultiMaterialTag|Altera as texturas|MultiChoiceRandomizer|Equipamentos e Sensores
MultiObjectTag|Reveza entre os objetos na base|MultiChoiceRandomizer|Equipamentos e Mostradores
RegionRandomizer|Altera a posição dentro de região|RegionTag|Objetos e câmera

### Labels

Geramos anotações dos dados de três tipos diferentes, bounding boxes, key points e bounding boxes 3D (posição e orientação 3D dos objetos).

## Modelo de detecção - Yolov5

Para detectar os objetos nas imagens, trainamos uma instância do modelo Yolov5. Esse modelo é capaz de extrair múltiplas bounding boxes de uma imagem, com boa eficiência.

Não realizamos o treino direto, mas sim transfer learning.

> **Transfer Learning**:
> 
> Técnica utilizada para treinar modelos de machine learning em que, em vez de começar o treinamento desde o início, continua a partir de um modelo pré-existente, permitindo aproveitar as features já extraídas por esse modelo. Acelera o processo de treino, podendo até congelar (impedir a alteração de pesos) de algumas camadas para isso, e pode melhorar a performance.

Utilizamos um [notebook pronto](https://colab.research.google.com/drive/1HQ4S2Av5ct2wIEpYamD2z5spA962XQo4?usp=sharing) para treinar o modelo, diponibilizado pelo Roboflow.

### Roboflow

Como os dados gerados pela Unity possuem uma organização própria (formato das anotações), decidimos utilizar a plataforma Roboflow para armazenar e converter os dados para a organização necessária para a YoloV5. O dataset obtido está público na plataforma: [GER-Drone-CBR-2021](https://universe.roboflow.com/ger/ger-drone-cbr-2021).

## Modelo dos mostradores - TF/Keras + ONXX + PyTorch

Como o objetivo dessa IA era detectar o conteúdo dos mostradores digitais do Desafio Petrobrás de Robótica, os dados foram gerados na Unity com o foco neles, para tanto, a compontente RegionTag do mostrador foi removida e no MultiObjectTag foram removidos os outros 2 objetos (vazio e equipamento) de modo que o mostrador estivesse sempre na mesma posição e que ele sempre estivesse na cena, só mudando sua rotação pelo AnguloTag. 

Além disso, é essencial centralizar a câmera de modo que não fique muito alta (perdendo a capacidade de visualizar os digítos como clareza), mas também não muito alta (para que a rede não perca generalização), para referência observe o DATASET no drive do GER, verifique também que os dados foram anotados como bounding-boxes para cada mostrador.

Depois de gerar o dataset e anotar os dados automaticamente pelo pacote .perception da Unity, é necessário utilizar o .json do data
