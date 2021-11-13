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

Utilizamos um [notebook pronto](https://colab.research.google.com/drive/1HQ4S2Av5ct2wIEpYamD2z5spA962XQo4?usp=sharing) para treinar o modelo, diponibilizado pelo Roboflow. Ele também está disponível na pasta `models`: [mostradores.ipynb](models/Treino%20YoloV5.ipynb).

### Roboflow

Como os dados gerados pela Unity possuem uma organização própria (formato das anotações), decidimos utilizar a plataforma Roboflow para armazenar e converter os dados para a organização necessária para a YoloV5. O dataset obtido está público na plataforma: [GER-Drone-CBR-2021](https://universe.roboflow.com/ger/ger-drone-cbr-2021).

## Modelo dos mostradores - TF/Keras + ONNX + PyTorch

Como o objetivo dessa IA era detectar o conteúdo dos mostradores digitais do Desafio Petrobrás de Robótica, os dados foram gerados na Unity com o foco neles, para tanto, a compontente RegionTag do mostrador foi removida e no MultiObjectTag foram removidos os outros 2 objetos (vazio e equipamento) de modo que o mostrador estivesse sempre na mesma posição e que ele sempre estivesse na cena, só mudando sua rotação pelo AnguloTag. 

Além disso, é essencial centralizar a câmera de modo que não fique muito alta (perdendo a capacidade de visualizar os digítos como clareza), mas também não muito alta (para que a rede não perca generalização), para referência observe o DATASET no drive do GER, verifique também que os dados foram anotados como bounding-boxes para cada mostrador. Não é possível gerar dois datasets diferentes com o mesmo seed, então sempre que acabassem as iterações, foi necessário certificar-se de mudar o seed no Game Component: Scenario da cena da Unity.

Depois de gerar o dataset e anotar os dados automaticamente pelo pacote .perception da Unity, foi necessário utilizar o .json da pasta do dataset gerado para cortar as imagens de forma que só apareça o mostrador na imagem, foi necessário mudar a imagem para grayscale e converter para tamanho 100x100 pixels para padronizar o dataset ao treinar a rede, e foi necessário separar todas as imagens em pastas baseadas nos dígitos do mostrador para ser possível aplicar um label em cada imagem para treinar a rede. Para referência utilizamos o códigos ```Pastas.py``` para criar essas pastas e ```Separador.py``` para separar as imagens nessas pastas, para usos futuros certifique-se de alterar as pastas alvo, dataset e a da última função (estão na pasta [exploration](exploration)).

Com as imagens devidamente cortadas, escaladas e separadas, foi necessário agora fazer o upload dessa pasta no Google Drive com o intuito de realizar o treinamento no Google Colab utilizando a back-end da GPU para melhoria de performance. Para isso, foi construído um modelo em TensorFlow utilizando a [API Funcional](https://www.tensorflow.org/guide/keras/functional), que essencialmente é muito parecida com a API Sequencial, utilizada nos tutoriais do TensorFlow, mas nela uma maior personalização das camadas é possibilitada, o que era essencial tendo em vista que a IA dos mostradores deveria ter varias saídas (uma para cada dígito e uma para o sinal), caracterizando o modelo como um classificador multiclasse.

É possível encontrar o notebook na pasta `models`: [mostradores.ipynb](models/mostradores.ipynb).

Apesar de a ideia inicial ser a de utilizar o modelo em TensorFlow, como a YoloV5 utilizava o PyTorch e as duas bibliotecas, no momento da competição, tinham duas versões conflitantes de uma biblioteca pré-requisito, não era possível fazê-lo. Para contornar o problema, foi feita a portabilidade do modelo de TensorFlow para [ONNX (Open Neural Network Exchange)](https://onnx.ai/) , um modelo de portabilidade das redes neurais entre si, e posteriormente para PyTorch. A conversão está explicada no notebook.

Após o treinamento do modelo, também foram feitos testes com novas imagens e feita uma matriz de confusão para verificar se o modelo é aceitável. Essa etapa também está exposta com mais detalhes no notebook.

> **Dicas para bom uso do Google Colab**
> 
>  **Uso limitado da Back-end da GPU**
> 
>   * Tendo em vista que o tempo de uso da Back-end da GPU das VM's do Google Colab é limitado, busque treinar o modelo em 100-200 epochs no começo, e faça o ajuste fino posteriormente após salvar esse primeiro modelo. 
> 
>    * Se você estiver treinando um modelo, não dê alt-tab para fora do navegador, o Colab pode simplesmente te desconectar, idealmente faça outra coisa em outra guia do navegador
> 
>   *   Se você estiver travado em alguma parte por querer alterar o código e não saber como, se desconecte da máquina virtual, o Colab pode te bloquear da GPU por ociosidade.
>   
>   * Infelizmente, se você deseja treinar o modelo, cuidado com eventuais saídas do computador, idealmente clique na tela do Colab de 5 em 5 minutos para evitar que o site considere que você está sendo ocioso.
> 
>   * Após uso prolongado do Colab (quase 1 hora e mais) o Google começa a te lançar alguns Captchas, esteja atento, se você demorar um pouquinho sequer para fazê-lo você será bloqueado da Back-end da GPU.
> 
>   * Os bloqueios da Back-end da GPU demoram entre 12 e 24 horas (em 2021), se você for bloqueado não entre em desespero.
> 
>   * Os bloqueios não são por IP mas sim por conta gmail, se você precisa desesperadamente acabar o modelo, logue em outra conta gmail no notebook e continue.
