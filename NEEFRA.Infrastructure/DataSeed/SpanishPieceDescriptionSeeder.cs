using MongoDB.Driver;
using NEEFRA.Core.Entities;

public static class SpanishPieceDescriptionSeeder
{
    public static void Seed(IMongoDatabase database)
    {
        var collection = database.GetCollection<SpanishPieceDescription>("SpanishPieceDescriptions");
        
        var count = collection.CountDocuments(_ => true);
        if (count > 0) return;

        var pieces = new List<SpanishPieceDescription>
        {
            new()
            {
                Name = "a_double_state",
                Description = "Esta doble estatua del dios Amón y su consorte Mut está tallada en granito. Data del Imperio Nuevo, probablemente de la Dinastía XIX bajo Seti I (padre de Ramsés II), y fue hallada también en el Templo de Karnak. Tanto Amón como Mut sostienen el signo anj —símbolo de la vida— en sus manos izquierdas. Las estatuas dobles que representan a un hombre y su esposa son comunes a lo largo de la historia del antiguo Egipto, y conllevan el significado simbólico del papel honorífico de la mujer en la sociedad egipcia, en contraste con los emperadores romanos, quienes raramente eran representados junto a sus esposas en estatuas pareadas. Mut aparece aquí del mismo tamaño que Amón. Su nombre en jeroglíficos también significa 'madre': ella es la arquetípica 'Madre Perfecta' de la Tríada Tebana. Lleva la Corona Doble del Alto y Bajo Egipto, y se la considera otra forma de 'Amunet' (consorte primordial de Amón). Era venerada en el recinto 'Isheru', al sur del templo de Karnak, donde se le dedicó un gran templo. Mut siempre está asociada al buitre, símbolo de protección divina; el signo jeroglífico de su nombre es una buitre hembra, razón por la cual las reinas egipcias llevaban el motivo del buitre en la cabeza bajo sus coronas."
            },

            new()
            {
                Name = "a_double_state_baster_and_his_wife",
                Description = "Estatua doble del funcionario Baser (también escrito Paser) y su esposa Hinout, tallada en granito gris. Hallada en la fortaleza de Tell el-Hebua, en el norte del Sinaí —el primer punto de control militar en el antiguo 'Camino de Horus', la carretera defensiva oriental de Egipto hacia Canaán. Baser desempeñó el cargo de Jefe de Arqueros, una alta posición militar responsable del mando de los arqueros egipcios durante las campañas en el Asia occidental. La pareja entrelaza los brazos en un gesto de afecto poco frecuente en la escultura egipcia, donde el contacto físico entre figuras transmitía profundos vínculos emocionales y sociales. Las estatuas dobles que representan a marido y mujer eran una tradición respetada en el arte del antiguo Egipto, que expresaba el significado simbólico del elevado papel de la mujer en la sociedad egipcia y honraba a la familia como fundamento de la vida en el más allá."
            },

            new()
            {
                Name = "A_statue_rameses_VI_B_state_Base_c_statue_prisoner",
                Description = "Estatua doble tallada en esquisto verde, hallada en Karnak, que representa a Ramsés VI (c. 1145–1137 a.C.) de pie ofreciendo una pequeña estatua del dios Amón como ofrenda votiva. El rey lleva la Corona Doble y fue esculpido a mayor escala que la estatua divina que está ofrendando. Sin embargo, el minucioso examen arqueológico reveló que el propietario original de la estatua no era Ramsés VI sino muy probablemente Ramsés V; al parecer, Ramsés VI se apropió de la estatua que su predecesor había dedicado a Karnak e inscribió su propio nombre en ella. La usurpación no se detuvo ahí: Ramsés VI también se apropió de la tumba de Ramsés V en el Valle de los Reyes (KV9). Esta tumba, una de las más bellas del Valle, fue iniciada por Ramsés V, quien completó la primera sección, antes de que Ramsés VI la terminara y la reclamara por entero. En una de las ironías de la historia, los obreros que excavaban esta tumba arrojaron los escombros sobre la entrada de la tumba de Tutankamón, ocultándola fortuitamente de los saqueadores hasta su descubrimiento en 1922. A pesar de las crisis económicas que azotaron Egipto durante su breve reinado de ocho años, la tumba de Ramsés VI es enorme, y su techo que representa a la diosa Nut está considerado uno de los techos pintados más magníficos del mundo."
            },

            new()
            {
                Name = "amenhotep_II_at_practice",
                Description = "Una gran estela de granito rosa (170 cm de alto, 234 cm de ancho) de la Dinastía XVIII (1439–1413 a.C.), hallada en Karnak en 1927 habiendo sido fragmentada y utilizada como relleno dentro del Tercer Pilono. Amenhotep II fue hijo del legendario guerrero Tutmosis III; habiendo llegado tras el constructor del mayor imperio de la historia, su papel fue principalmente mantener esta herencia. Para demostrar su propio valor independiente, se centró en exhibir una extraordinaria fuerza física. En esta estela, el rey aparece conduciendo su carro a toda velocidad, sosteniendo su arco y disparando flechas contra una diana de cobre (no la habitual de madera), demostrando a todos que superaba a los demás hombres. El texto que la acompaña describe las habilidades del rey: sus flechas atravesaron los blancos de cobre 'como si fueran papiro'. Los caballos que tiran del carro están esculpidos con una habilidad que refleja su poder y velocidad. Sobre los caballos, dos textos describen sus extraordinarias capacidades; abajo, otro texto describe su precisión al alcanzar el blanco. Una estela comparable en el Museo de El Cairo le muestra golpeando a enemigos y presentando prisioneros a Amón. Cabe destacar que la momia de este rey fue hallada en su tumba (KV35) con su arco personal a su lado —del que presumía que nadie más podía tensar."
            },

            new()
            {
                Name = "amenhotep_III_crowned_by_amon_ra",
                Description = "Una estatua de granito representa la coronación de Amenhotep III (1409–1367 a.C.): el joven rey arrodillado ante Amón, quien coloca su mano sobre la corona del rey. Los brazos y las piernas del dios Amón han sido deliberadamente destruidos —daños infligidos durante la revolución religiosa de Akhenatón— mientras que la estatua de su padre Amenhotep III quedó intacta, ilustrando el carácter selectivo del iconoclasmo de Amarna. Las partes faltantes fueron restauradas posteriormente en yeso."
            },

            new()
            {
                Name = "Amenhotep_IV",
                Description = "En todo museo se encuentra una sección dedicada a Tell el-Amarna; aunque este período duró apenas treinta años, dejó huellas indelebles en el arte, la religión y la política. Ninguna figura real ha atraído tanta atención académica como Akhenatón. El rey comenzó su reinado bajo el nombre de Amenhotep IV antes de cambiarlo. Antes de su era, los reyes egipcios eran representados con una imagen idealizada que combinaba un físico poderoso con rasgos armoniosos. Pero en los primeros años de su reinado comenzó a aparecer un estilo audaz y completamente diferente en la escultura real. Las estatuas ahora expuestas fueron descubiertas en su templo en Karnak —un templo que construyó al dios Atón al este del complejo de Karnak en los primeros años de su reinado, antes de que el conflicto religioso con los sacerdotes de Amón se agravara. Lo asombroso es que estas estatuas violaron tradiciones artísticas heredadas durante miles de años: por primera vez, el rey aparece —por propia voluntad— en un estilo cercano a la caricatura miles de años antes de que la caricatura existiera como forma artística, y en algunas esculturas aparece desnudo en la postura osiriana. El rostro de Akhenatón en estas estatuas presenta rasgos exagerados e inusuales: un rostro excesivamente alargado y una frente huidiza; ojos estrechos que sugieren profunda contemplación; una nariz larga con profundos surcos nasogenianos; labios carnosos y una mandíbula anormalmente prominente. Esta imagen se alineaba con su revolución religiosa: rechazó la tradición y llamó a la devoción absoluta al poder del sol concentrado en el disco de Atón. En el sexto año de su reinado, permanecer en Tebas (bastión de Amón) se volvió imposible, y fundó su nueva capital 'Akhet-Atón' (Horizonte de Atón). Notablemente, Akhenatón supervisó personalmente a sus artistas en la concepción de este estilo único. Con su muerte la religión desapareció, considerada una 'herejía religiosa' que los sacerdotes posteriores intentaron borrar por completo. Su mayor error, quizás, fue vincular la religión a su propia persona —convirtiéndose en el único intermediario entre el pueblo y el dios, de modo que con su muerte el vínculo se rompió y el movimiento murió."
            },

            new()
            {
                Name = "amenhotep_IV_with_Double_crown_geraniet",
                Description = "Estatua de granito de Amenhotep IV (conocido posteriormente como Akhenatón) que lleva la tradicional Corona Doble (Pschent) —la corona combinada del Alto y Bajo Egipto. A pesar del símbolo real tradicional de la Corona Doble, los rasgos faciales de la estatua exhiben el distintivo estilo exagerado de Amarna: un rostro alargado con frente huidiza, ojos estrechos contemplativos, nariz larga con prominentes surcos nasogenianos, labios carnosos y mandíbula anormalmente prominente. Esta estatua fue descubierta en su templo de Atón construido al este del complejo de Karnak en los primeros años de su reinado, antes de que la revolución religiosa escalara por completo. Ilustra la naturaleza de transición del arte del período de Amarna temprano: coronas reales tradicionales combinadas con un vocabulario artístico radicalmente nuevo que Akhenatón supervisó personalmente."
            },

            new()
            {
                Name = "amenhotep_IV_with_sand",
                Description = "Media estatua del rey Akhenatón (Amenhotep IV) que lleva la Corona Doble y sostiene el cayado y el mayal reales —los tradicionales símbolos de la autoridad faraónica y la fertilidad. Tallada en el característico estilo de Amarna, los exagerados rasgos físicos de la estatua reflejan la revolución filosófica y religiosa personal de Akhenatón: el rostro alargado, los labios carnosos y la mandíbula prominente no eran distorsiones sino expresiones artísticas deliberadas de la trascendencia divina. Hallada en su templo de Atón en Karnak, la estatua representa al rey en la postura osiriana típica de las medias estatuas —una forma utilizada en contextos religiosos. En el sexto año de su reinado, permanecer en Tebas se volvió imposible dado el conflicto con el poderoso sacerdocio de Amón, y fundó su nueva capital Akhet-Atón (la moderna Amarna). Tras su muerte, sus sucesores borraron sistemáticamente esta revolución artística y religiosa, recuperando los dioses tradicionales y las convenciones artísticas."
            },

            new()
            {
                Name = "Ancient_Egyptian_Faience_Glass_Objects_and_Ushabti_Figures",
                Description = "La primera vitrina está dedicada a objetos de vidrio y fayenza. La fayenza es piedra de cuarzo en polvo mezclada con ciertos tipos de arcilla, óxidos metálicos, cal y arena blanca. Estos componentes se mezclan en proporciones específicas para formar una pasta con la que se fabrica un tipo de cerámica ampliamente utilizada en el antiguo Egipto. Esta cerámica se distinguía por sus variados colores, especialmente el azul y el verde. Con ella se elaboraban figurillas, joyas, vasijas, e incluso los templos a veces se revestían con azulejos de fayenza. El descubrimiento más antiguo conocido de fayenza se encuentra en la 'Cámara Azul' de la Pirámide Escalonada de Saqqara. El término moderno 'Faience' deriva del nombre de la ciudad de Faenza, situada en Italia. La fayenza se utilizaba principalmente en el ajuar funerario, donde se elaboraban amuletos y pequeñas estatuillas funerarias (ushebtis). La vitrina contiene: (A) Cuatro vasos de vidrio de Deir el-Medina. (B) Una esfinge de fayenza azul del rey Horemheb. (C) Un alfiler de fayenza del reinado de Ramsés III. (D) Cuentas de fayenza. (E) Una pequeña figurilla de fayenza. (F) Una estatuilla funeraria ushebti de un escriba llamado 'User-nakht'. (G) Un trozo de pasta de vidrio cruda."
            },

            new()
            {
                Name = "Ancient_Egyptian_Masonry_and_Measuring_Tools",
                Description = "Pasamos ahora a la vitrina de herramientas de medición. Los antiguos egipcios utilizaban sin duda instrumentos precisos y variados en sus obras de construcción. En una de las tumbas de Deir el-Medina —perteneciente a uno de los artistas que participó en la construcción de las tumbas reales en el Valle de los Reyes— se descubrieron numerosas herramientas de construcción, entre ellas las expuestas en esta vitrina: (A) Una pequeña estatua del arquitecto Senenmut, Director de Obras de la reina Hatshepsut, sosteniendo un cartucho con el nombre de la reina. (B) Una herramienta que representa la medida del codo egipcio. (C) Otra sección de una medida de codo egipcio con el cartucho del rey Nectanebo II, hallada en Karnak. (D) Una escuadra de madera de la Dinastía XVIII. (E) Una herramienta en forma de A con cuerdas y una piedra colgante, usada para nivelar superficies horizontales. (F) Una plomada de la Dinastía XIX, de madera y piedra. (G) Una herramienta de madera para comprobar la verticalidad de superficies."
            },

            new()
            {
                Name = "Ancient_Egyptian_Metalworking_and_Bronze_Statuettes",
                Description = "Pasamos a la segunda vitrina, conocida como la 'Vitrina de los Metales', relacionada con la historia y el desarrollo de la metalurgia en el antiguo Egipto. Los antiguos egipcios demostraron maestría en muchos oficios desde tiempos prehistóricos. Sin embargo, la metalurgia no fue inicialmente prominente: el cobre apareció lentamente en el período prehistórico tardío; el uso del bronce no comenzó hasta alrededor del 2000 a.C.; y el hierro entró en las industrias egipcias lentamente entre 1000 y 600 a.C. Egipto era tan pobre en madera como en metales, por lo que los egipcios sustituyeron la piedra por herramientas que requerían dureza. A pesar de no haber inventado las técnicas fundamentales de la metalurgia, Egipto produjo objetos de cobre de exquisita belleza. La metalurgia estaba bajo la supervisión directa del Estado y los templos. Los antiguos egipcios usaban el brillante método de la 'cera perdida' para fundir estatuas de bronce: el artista crea un modelo en arcilla, lo recubre con cera sobre la que esculpe los detalles, aplica una capa exterior de arcilla, calienta el molde hasta que la cera fluye hacia fuera, y luego vierte metal fundido para reemplazar la cera. La vitrina contiene: (A) Una figurilla de arcilla de la tumba de Ramsés XI. (B) Una figurilla de cera también de la tumba de Ramsés XI (etapas preparatorias de la fundición a la cera perdida). Tres estatuas de bronce terminadas: una del dios Amón; una del dios Horus como halcón; y una de la diosa Isis amamantando a su hijo Horus."
            },

            new()
            {
                Name = "Ancient_Egyptian_Scribe's_Palette_Painting_Tools",
                Description = "Así como la civilización en la historia mundial comienza con el conocimiento de la agricultura, la historia comienza indudablemente con la invención de la escritura. Egipto estuvo entre las primeras civilizaciones en utilizar la escritura en la vida cotidiana. Desde el período Naqada I (c. 4000 a.C.), se escribían símbolos en cerámica, y para el 3200 a.C. el nombre del rey aparecía escrito en la Paleta de Narmer. Las escuelas se extendieron por todo Egipto, incluso en las aldeas. El dios Thoth era considerado el dios de la sabiduría y guardián de todo el conocimiento. Las herramientas del escriba consistían en: la Paleta de Escritura (con dos huecos cóncavos para tinta negra y roja), un recipiente de agua para enjuagar las plumas, y una caja para guardar plumas y tintas. La vitrina contiene: (A) Una estatuilla de madera y bronce del dios Thoth en forma de ibis, del período grecorromano, hallada en Tuna el-Gebel. El ibis era asociado con el tribunal de Osiris en el más allá. El granjero egipcio lo llamaba 'Abu Minjal' (padre de la hoz) por su pico curvado, y admiraba su capacidad para detectar gusanos ocultos en el barro, viéndolo como un don divino. (B) Una tabla de madera para pigmentos. (C) Un estuche de marfil para plumas con residuos de pigmentos negro y rojo. (D) Un bloque de óxido de manganeso negro y rojo para producir tintas. (F) Un estuche para guardar plumas y tintas."
            },

            new()
            {
                Name = "Ancient_Egyptian_Stone_Lintel",
                Description = "Un dintel de arenisca de la Dinastía XVIII que porta dos cartuchos de Tutmosis III, flanqueados a cada lado por un sacerdote en postura de adoración mirando al cartucho. El texto del lado derecho dice: 'Primer Servidor del Dios de (Men-kheper-Ra), el justificado, nacido de (Tuosret)', mientras que el de la izquierda dice: 'Primer Servidor del Dios de (Men-kheper-Ra), Khonsu, el justificado, nacido de (Tuosret)'. Cerca se expone un dintel similar de caliza con texto comparable y trazas de pintura roja, verde y amarilla."
            },

            new()
            {
                Name = "Ancient_Egyptian_Wooden_Model_Boats",
                Description = "Aquí tenemos un modelo de barca ceremonial de madera pintada, datado en la Dinastía XVIII (Imperio Nuevo). Esta barca fue encontrada en la tumba del rey Amenhotep II, colocada allí para uso del rey en el más allá. La barca está decorada con figuras que representan al dios Montu (el antiguo dios de Tebas y dios de la guerra), representado como una esfinge con cabeza de carnero aplastando a enemigos nubios y asiáticos. El río Nilo era, y sigue siendo, el sustento vital que conecta Egipto de un extremo al otro. Las primeras barcas eran de papiro; posteriormente los egipcios usaron madera, inicialmente local y luego cedro del Líbano importado desde el reinado del rey Sneferu. Los egipcios inventaron maneras de erigir mástiles, sujetar velas y dirigirlas usando remos, lo que les permitió navegar por el Nilo, el Mar Rojo y el Mediterráneo. Los dos modelos de barca expuestos aquí datan del Imperio Medio y fueron descubiertos en las tumbas del distrito de Meir en el Gobernato de Asyut. Cada barca contiene un juego de remos, indicando posición de navegación hacia el sur (contra la corriente). La tripulación consiste en remeros, un capitán siempre de pie en la proa y un supervisor en la popa."
            },

            new()
            {
                Name = "ancient_Egyptians_went_to_secure_their_journey_to_the_afterlife",
                Description = "Diversos objetos que reflejan aspectos de la vida y la creencia en el antiguo Egipto: (A) Fruta seca hallada en tumbas, incluyendo higos, plátanos y uvas. (B) Una pequeña figura con cuerpo humano y cabeza de ibis —una de las formas del dios Thoth. (C) Semillas y granos: chufa, altramuz, trigo y cebada. (D) Una importante figurilla ushebti de un hombre llamado 'Setau', tallada en madera pintada, de 22,7 cm de altura, de la Dinastía XVIII tardía (c. 1400–1360 a.C.). Setau ostentaba el título de 'Servidor en el Lugar de la Justicia en la Necrópolis Real'. La figurilla lo muestra sosteniendo una azada en la mano derecha y una bolsa de semillas en la izquierda, con inscripciones del Capítulo 6 del Libro de los Muertos. (E) Lámparas decoradas con motivos vegetales. (G) Parte de un marco que representa a dos mujeres con una flor de loto. (H) Un modelo de noria. (I) Una cesta ovalada tejida con hojas de palmera. (J) Un ramo de flores funerarias secas. (K) Parte de una pared que representa a un hombre recogiendo figurillas ushebti, hallada en 2017 en la tumba n.º 150 en Dra Abu el-Naga. Las figurillas ushebti son esenciales en el ajuar funerario —la palabra significa 'el que responde'. Cuando se llamaba al difunto a realizar trabajos forzados en los campos de Iaru (el Paraíso), la figurilla respondía en su nombre: '¡Aquí estoy, lo haré por ti!' La colección expuesta consiste en 1.000 pequeñas figurillas de fayenza azul halladas por la misión egipcia en Tuna el-Gebel en 2019."
            },

            new()
            {
                Name = "another_state_of_rameses_III",
                Description = "Estatua del rey Ramsés III en esquisto, perteneciente a la Dinastía XX, descubierta en el complejo del templo de Karnak. La estatua representa a Ramsés III en pose devocional, llevando una peluca corta coronada por la doble corona, de pie ante el dios Osiris. Al lado del rey, tras su pierna izquierda, se encuentra una figura más pequeña de un príncipe y comandante del ejército. Esta estatua fue descubierta por primera vez en 1930 por la expedición del Instituto Oriental de Chicago; fragmentos adicionales fueron hallados en 2002 por una expedición de la Universidad Johns Hopkins bajo el suelo del Templo de Mut en Karnak, y todas las piezas fueron completamente restauradas en 2003. Ramsés III rechazó con éxito dos grandes invasiones libias y un tercer asalto de los 'Pueblos del Mar' —una confederación de pueblos mediterráneos que ya habían derrocado al Imperio Hitita— defendiendo con éxito a Egipto de un destino que sufrieron todas las demás grandes potencias de la Edad del Bronce."
            },

            new()
            {
                Name = "architectural_lintel_tuhutmusis_III",
                Description = "Otra pieza de la expedición polaca en Deir el-Bahari: un bloque de caliza pintada que porta tres cartuchos de Tutmosis III y su abuelo Tutmosis I. Los antiguos egipcios imaginaban el universo como todo aquello encerrado por la luz del sol, y concibieron el signo 'Shenu' para expresar este concepto —una cuerda anudada formando un círculo que simboliza el disco solar. Escrito dentro de este marco ovalado, el nombre del rey proclamaba que el mundo entero pertenecía al faraón. Esta tradición persistió en la era islámica; sobrevive un cartucho del sultán Barquq. En el antiguo Egipto los cartuchos eran rectángulos alargados para acomodar signos jeroglíficos altos. Los cartuchos normalmente portaban dos de los cinco nombres del rey (nombre de trono y nombre de nacimiento), y fueron precisamente estos cartuchos los que dieron a Champollion la clave para descifrar la lengua del antiguo Egipto."
            },

            new()
            {
                Name = "Architectural_Ostra_a_with_Building_Plans",
                Description = "Los 'ostraca' son fragmentos de cerámica o lascas de caliza utilizadas para escribir y dibujar por su fácil disponibilidad. Se exhiben piezas que portan cartas personales, cuentas, extractos literarios y bocetos de rostros humanos, elementos arquitectónicos y procesiones funerarias. Un fragmento representa un taller; otro lleva un dibujo de la 'Cabeza de Horus'. También se muestra un pequeño recipiente de la Dinastía XVII que contiene cascarillas de cebada y frutos de palma datilera —'equipamiento' para elaborar cerveza en el más allá para el difunto, pues se creía que el ka (espíritu) del difunto se nutriría de la esencia espiritual de estas ofrendas."
            },

            new()
            {
                Name = "base_with_nine_bows",
                Description = "La exposición incluye representaciones de cautivos: los reyes egipcios acostumbraban representar a sus enemigos atados, con las manos a la espalda dentro de un óvalo almenado que simbolizaba una fortaleza, con el nombre de su tierra escrito dentro. Los egipcios usaban dos números para expresar la victoria absoluta: el número 4 (los cuatro puntos cardinales) y el número 9, encarnado en los 'Nueve Arcos' —el plural de plurales, que expresa a todos los enemigos de Egipto. Estos arcos están tallados bajo los pies de las estatuas reales para simbolizar el aplastamiento y la destrucción de los enemigos. Los egipcios usaban dos palabras para describir al enemigo: 'Sbi' (el rebelde) y 'Khefty' (el enemigo externo que nunca fue sometido, aplicado habitualmente a los asiáticos). Entre los ejemplos expuestos: una estatua de Ramsés VI sosteniendo el cabello de un cautivo libio con un hacha de batalla en su mano derecha; una estatua de granito negro de un cautivo en posición prona con las manos atadas; y una base de granito negro de Medinet Habu tallada con cabezas de cautivos."
            },

            new()
            {
                Name = "Belisk_of_rameses_III",
                Description = "Un obelisco de granito rojo de Ramsés III, de 95,5 cm de altura, hallado en 1923 en el lado occidental del patio entre el Noveno y el Décimo Pilono en Karnak. Sus cuatro caras llevan textos jeroglíficos con los nombres y títulos del rey. El sol era el elemento natural más importante para los antiguos egipcios; lo veneraban como el dios Ra y erigían delgados obeliscos de piedra en su honor. Las puntas de estos obeliscos se recubrían de oro o electro para reflejar los primeros rayos del sol de la mañana, proclamando el retorno del dios de su viaje nocturno por el inframundo. Los gobernantes del Imperio Nuevo erigieron obeliscos colosales como expresión de devoción absoluta al dios sol; los obeliscos de Tutmosis I y Hatshepsut se alzan orgullosos en Karnak hasta hoy, a diferencia de otros transportados para adornar plazas europeas y americanas. Lo notable de este obelisco es que Ramsés III proporcionó tres versiones diferentes de su nombre de Horus —el único rey que lo hizo. Los egiptólogos no han encontrado hasta hoy una explicación satisfactoria a este fenómeno único."
            },

            new()
            {
                Name = "block_state_of_amenhotep_sonof_habu",
                Description = "Estatua-bloque de Amenhotep hijo de Hapu, uno de los personajes no reales más famosos de la historia egipcia. Ascendió a la prominencia bajo Amenhotep III, convirtiéndose en Supervisor de Todas las Obras Reales, y vivió más de 80 años. Se encuentra en la 'Sala Militar' porque en la primera parte de su carrera fue el alto funcionario responsable de reclutar y preparar a los jóvenes para el servicio militar —el hombre que suministraba los recursos humanos que construyeron el imperio. También es conocido como el brillante arquitecto que supervisó la construcción de la mayor parte del Templo de Luxor, y se le concedió el raro honor de un templo funerario propio en la Orilla Occidental —una distinción raramente otorgada a funcionarios de alto rango."
            },

            new()
            {
                Name = "block_state_of_nespeka_shuty",
                Description = "Una 'estatua-bloque' de caliza del visir Nes-Bakashuty, del Período Intermedio Tardío (Dinastía XXII), concretamente del reinado de Sheshonq III (c. siglo VIII a.C.). Hallada en el Escondrijo de Karnak en 1904. Las 'estatuas-bloque' son una forma artística exclusivamente egipcia que apareció en el Imperio Medio y floreció en el Imperio Nuevo y períodos posteriores —una forma sin equivalente en ninguna otra civilización antigua. Representan a una figura sentada envuelta apretadamente en un manto que cubre todo el cuerpo, con solo la cabeza y las manos visibles, mientras los pies se fusionan con la base. Nes-Bakashuty lleva una peluca, y sus orejas son notablemente más grandes de lo habitual —una alusión simbólica a su deseo de escuchar los himnos y oraciones recitados en el templo. Las inscripciones nos dicen que era visir y sacerdote tanto de Amón como de Maat, y en virtud de este último cargo desempeñaba funciones de juez. La estatua porta dos emblemas sagrados: los sistros de la diosa Hathor, tallados en su frente; y la planta de lechuga del dios Min sostenida en la mano derecha."
            },

            new()
            {
                Name = "block_state_of_yamunedjeh",
                Description = "Presentamos aquí una estatua de granito negro de una persona llamada 'Yamu-Nedjeh'. Esta estatua data del Imperio Nuevo, Dinastía XVIII, en el reinado tardío del rey Tutmosis III (c. 1460 a.C.). La estatua mide 96,5 cm de altura. Fue hallada en la zona de Qurna en 1933, a unos 200 metros al noreste de la entrada principal del Templo Funerario del rey Tutmosis III. Una 'estatua-bloque' es un estilo específico de estatua que representa a un hombre sentado con las rodillas recogidas hasta el pecho y los brazos cruzados sobre ellas; este estilo es una invención artística única de la civilización egipcia que no tiene equivalente en ninguna otra. Yamu-Nedjeh era uno de los funcionarios más destacados durante el reinado del rey Tutmosis III. Las inscripciones confirman que ostentaba el cargo de 'Primer Heraldo Real' (Mensajero Real) y era el supervisor arquitectónico de todos los edificios del rey. Militarmente, acompañó al rey en su campaña en el Asia occidental cruzando el Éufrates en el trigésimo tercer año del reinado del rey. Como 'Supervisor de Obras', supervisó personalmente la erección de tres pares de obeliscos para el rey —dos dedicados al dios Amón en Karnak, y cuatro en Heliópolis en honor al dios Atum."
            },

            new()
            {
                Name = "ceremonial_axe_of_ahmose",
                Description = "También se expone el hacha conmemorativa del rey Ahmose —una obra maestra arqueológica de oro, electro y cobre, incrustada con piedras semipreciosas y materiales de madera. Data del Imperio Nuevo, Dinastía XVIII, y fue descubierta en la zona de Dra Abu el-Naga, dentro de la tumba de la reina Ahhotep. Esta hacha conmemora las celebraciones de Egipto tras la gloriosa victoria del rey Ahmose sobre los hicsos. Presenta inscripciones con el nombre y los títulos del rey Ahmose, junto con una escena que representa al rey sometiendo a enemigos asiáticos, y textos con oraciones y deseos para la larga duración de su reinado."
            },

            new()
            {
                Name = "collection_of_antiquities_belongs_to_the_family_of_priest_Ankh_ef_en_Khonsu",
                Description = "Una colección de pequeñas antigüedades descubiertas en la tumba del sacerdote Ankh-ef-en-Khonsu, que sirvió en el Templo de Amón en Karnak durante la Dinastía XXII (c. 945–715 a.C.). El nombre del sacerdote significa 'El que vive para Khonsu', el dios lunar de la Tríada Tebana junto a Amón y Mut. La colección incluye objetos funerarios personales: una estatuilla de bronce del dios Osiris, un ushebti de fayenza azul inscrito con el nombre y títulos del sacerdote, un par de pendientes de plata que reflejan los estilos de joyería del Período Intermedio Tardío, y una pequeña tablilla de madera para ofrendas. Estos objetos ilustran la devoción religiosa y la cultura material de la clase sacerdotal tebana. El Período Intermedio Tardío vio a poderosas familias sacerdotales gobernar efectivamente el Alto Egipto, rivalizando a veces con la autoridad faraónica de las dinastías del norte."
            },

            new()
            {
                Name = "colossal_statue_of_seti_I",
                Description = "Estatua del rey Seti I (padre de Ramsés II) en alabastro. En mal estado y mantenida con abrazaderas de hierro y mortero de cemento, fue restaurada científicamente en 2003: se eliminaron los antiguos cementos y soportes, la piedra se consolidó con silicato de etilo, las partes faltantes se completaron con poliéster y mortero de cal para armonizar con la forma general, y se añadieron soportes no corrosivos para garantizar el equilibrio de la estatua."
            },

            new()
            {
                Name = "column_from_sand_stone",
                Description = "Una columna estriada de arenisca que representa la mención más antigua conocida del nombre del dios Amón en Karnak. Hallada como relleno dentro del Tercer Pilono, data del Primer Período Intermedio, concretamente del reinado de Wahankh Intef II —uno de los primeros gobernantes de la Dinastía XI. Esta columna es una sólida evidencia arqueológica de que el culto de Amón-Ra en Karnak ya estaba establecido en el Imperio Medio, aunque 'Montu' era sin duda la deidad más antigua y principal de Tebas antes de que Amón adquiriera prominencia."
            },

            new()
            {
                Name = "column_of_god_amon",
                Description = "Un pilar decorado con inscripciones e imágenes en relieve realzado relacionadas con el dios Amón —el Rey de los Dioses, el Oculto y deidad suprema de Tebas a lo largo del Imperio Nuevo. El nombre de Amón en antiguo egipcio significa 'el Oculto', reflejando su naturaleza como fuerza cósmica invisible que permea todas las cosas. Solía representarse como un hombre con una corona de doble pluma, asociado con el poder creador del sol y la fertilidad del aire y el viento. Su gran complejo templario en Karnak —el mayor complejo de edificios religiosos en la historia del mundo, abarcando más de 200 acres— fue construido y ampliado durante 2.000 años por sucesivos faraones que buscaban la aprobación divina. Las inscripciones jeroglíficas del pilar proclaman la gloria de Amón y la devoción del rey que lo encargó, reflejando el vínculo inseparable entre la autoridad faraónica y el patrocinio divino de Amón."
            },

            new()
            {
                Name = "Coptic tombstone",
                Description = "La sala contiene varias estelas funerarias coptas (siglos VI–VII d.C.) talladas en arenisca. Una estela de la 'Victoria' presenta una parte superior redondeada con un águila de alas extendidas (símbolo de victoria), dos cruces y una tableta con inscripción que porta una leyenda griega: 'El Único Dios que ayuda —Jesucristo... que sea victorioso' —es decir, victoria sobre la muerte. Una estela 'Alfa y Omega' con parte superior triangular contiene una cruz flanqueada por la primera y última letra del alfabeto griego, símbolos de Cristo. Su inscripción reza: 'El Único Dios que ayuda —el monje (Padre Amín)'. Un panel decorativo que se cree fue parte de una pared de iglesia muestra dos columnas con capiteles corintios flanqueando un nicho que contiene un pez (símbolo cristiano primitivo), un pavo real y un animal de cuatro patas. También se muestra un fragmento de ábside arquitectónico de caliza, hallado en 1958 en el Primer Patio del Templo de Luxor, decorado con una corona de laurel y una concha con un águila de la victoria —probablemente parte de una de las seis iglesias que existían alrededor del templo."
            },

            new()
            {
                Name = "coptic_grave_stone",
                Description = "Un ataúd de yeso pintado, lino y madera del Período Intermedio Tardío, hallado en el-Qurna en la tumba de Mentuhotep (utilizada como panteón familiar). Pertenece a un sacerdote de Montu llamado Nes-Bakashuty —un individuo diferente al visir Nes-Bakashuty de la estatua-bloque. Cerca se descubrieron seis candelabros de bronce durante las excavaciones en la zona de la Esfinge al norte del Primer Pilono del Templo de Luxor. Datando del siglo VIII d.C. (período islámico temprano), se usaban como implementos litúrgicos en las iglesias establecidas dentro del Templo de Luxor. Cada candelabro consiste en un fuste ricamente decorado sobre una base de tres patas, fundido como una sola pieza de bronce con una ranura en la parte superior para la vela. Estos candelabros son una importante adición al corpus de metalurgia cristiana del Alto Egipto."
            },

            new()
            {
                Name = "dagger_and_sheath_of_ahmose",
                Description = "Una daga ceremonial perteneciente al rey Ahmose I, fundador de la Dinastía XVIII y libertador de Egipto de la ocupación hicsa. La daga está fabricada en bronce con un mango revestido de oro, incrustado con lapislázuli y cornalina —piedras semipreciosas cuyos vibrantes colores azul y rojo tenían un poderoso significado simbólico en el arte del antiguo Egipto. La ornamentada vaina de madera está recubierta con patrones geométricos de lámina de oro, representando una escena del rey sometiendo a un enemigo asiático —la imagen clásica del triunfo faraónico. Hallada en la tumba de la reina Ahhotep en Dra Abu el-Naga junto al collar de 'Moscas del Valor' de su madre, esta arma representa el nacimiento del Imperio Nuevo y el renacimiento del poder militar egipcio tras más de un siglo de dominio hicso. Su extraordinaria artesanía refleja el renacimiento artístico que acompañó a la liberación de Egipto."
            },

            new()
            {
                Name = "Early_christian_stela",
                Description = "Una estela del período cristiano primitivo en Egipto (siglos VI–VII d.C.), tallada en arenisca y que representa la transición de las tradiciones religiosas del antiguo Egipto al período cristiano en el Alto Egipto. La estela presenta motivos cristianos simbólicos —cruces, símbolos de peces e inscripciones griegas que reflejan las creencias tempranas sobre la muerte, la resurrección y la vida eterna. Tales estelas proporcionan evidencia del rico patrimonio cristiano de Luxor y el Alto Egipto durante el período bizantino, cuando el Templo de Luxor y sus zonas circundantes albergaban múltiples iglesias dentro de lo que antaño fueron sagrados espacios faraónicos. El período cristiano primitivo en Egipto (la era copta) representa un puente cultural y espiritual vital entre la antigua civilización faraónica y el período islámico que siguió, formando una de las tres grandes capas del extraordinario legado histórico de Luxor."
            },

            new()
            {
                Name = "false_door_stela",
                Description = "Una falsa puerta ptolemaica de arenisca —un elemento auténticamente egipcio considerado como puerta al más allá— que consiste en una cornisa cóncava y columnas de papiro, con trazas de pintura roja y yeso blanco. Si este panel era una ofrenda votiva en lugar de un marcador funerario, la abertura central puede haber albergado una imagen divina. Finalmente, una pieza única que representa la fusión artística grecoegipcía (siglo II a.C.): un gran bloque de piedra que representa a un hombre desnudo con una corona de hojas de vid, recostado en un diván griego y sosteniendo un racimo de uvas. El bloque porta un texto jeroglífico que desea 'buena fortuna' e imagen de una serpiente (la diosa Renenutet). Una apertura semicircular en la base del diván sugiere que este bloque formaba parte de un lagar, siendo la figura representada el dios Dioniso (el dios griego del vino), con el orificio diseñado para permitir que fluyera el jugo."
            },

            new()
            {
                Name = "Fragment_Of_wall_Thutmosis_II_and_hatshepsut",
                Description = "Una sección de decoración mural de la Dinastía XVIII que representa al rey Tutmosis II y a la reina Hatshepsut juntos —una de las pocas imágenes supervivientes de esta pareja real. Ambas figuras aparecen adorando al dios Amón y presentando ofrendas en la manera ritual prescrita. Este fragmento proporciona una rara documentación visual del período de corregencia entre Tutmosis II y su reina Hatshepsut, quien posteriormente se convirtió en faraón por derecho propio. El estilo artístico es característico de la Dinastía XVIII temprana, mostrando la evolución gradual desde las tradiciones artísticas del Imperio Medio hacia el estilo tutmósida clásico. La poderosa influencia religiosa de Hatshepsut ya es evidente en los relieves murales del reinado de su esposo, presagiando el período en que asumiría los cinco títulos faraónicos y gobernaría Egipto de forma independiente durante más de veinte años."
            },

            new()
            {
                Name = "gold_coins",
                Description = "Aunque la importancia política de Luxor en el período islámico disminuyó en comparación con su apogeo faraónico, las excavaciones revelaron objetos únicos, incluida una vasija cerámica de la época mameluca con un esmalte verde semitransparente, descubierta durante excavaciones a lo largo de la Avenida de las Esfinges al norte del Templo de Luxor. Este tipo de cerámica imitaba las creaciones de las dinastías Yuan y Ming en China. También se incluyen pequeñas vasijas de arcilla roja de los períodos mameluco y fatimí, con esmaltes que van desde el verde vivo al amarillo. De la colección islámica destaca también un collar de plata de quince piezas geométricas (en forma de diamante) que termina en un colgante en forma de media luna decorado con fina granulación y ornamentos entrelazados. Las piezas del collar fueron encontradas desmontadas en 1962 en el Templo de Luxor y recientemente fueron vueltas a ensamblar."
            },

            new()
            {
                Name = "head_of_amenhoteb_III",
                Description = "Esta cabeza fue hallada en 1964 entre las ruinas del templo funerario del rey en la Orilla Occidental de Luxor —el templo que se alza detrás de los famosos Colosos de Memnón, que originalmente representaban al propio Amenhotep III. La estatua fue encontrada en muy mal estado y fue restaurada por el mismo equipo que trabajó en los Colosos de Memnón. Esta cabeza representa a uno de los reyes más ricos del antiguo Egipto, quien heredó un imperio en el apogeo de su prosperidad —construido por grandes predecesores como Hatshepsut, Tutmosis III, Amenhotep II y Tutmosis IV. Era un joven que amaba la caza y se jactaba de haber matado 102 leones, pero en la segunda mitad de su vida se sumergió en el lujo y el placer. No era propenso a la guerra; las colonias egipcias en Asia eran estables y el Estado era lo suficientemente fuerte como para que nadie se atreviera a rebelarse. Esta estatua, de más de 3.400 años de antigüedad, está fabricada en cuarcita —una forma dura de arenisca en la que se han formado cristales de cuarzo, que va del blanco al amarillo pálido, y aún hoy se extrae del área de la 'Montaña Roja' al noreste de El Cairo."
            },

            new()
            {
                Name = "head_of_amenhoteb_III_kuar",
                Description = "Un pequeño león de arenisca pintada del 'Período Arcaico' (Dinastías I o II, c. 3000 a.C.), hallado en 1974 entre el Octavo y Noveno Pilono en Karnak. Probablemente es una antigua ofrenda votiva donada al templo en un período posterior. En su espalda son visibles surcos desgastados por peregrinos que rasparon partículas de arena para usarlas como reliquias sagradas. Cerca se encuentran tres importantes cabezas reales: una cabeza de Amenhotep III en cuarcita con la doble corona; una cabeza de Tutmosis III en caliza; y una cabeza de Amenhotep II en cuarcita roja. Es evidente una fina distinción en el modelado facial —los párpados de Amenhotep II descienden pronunciadamente hacia la nariz en comparación con los párpados más suavemente arqueados de su padre Tutmosis III, y los rasgos de Tutmosis III son más nítidos y definidos en general."
            },

            new()
            {
                Name = "head_of_amenhotep_I",
                Description = "Una cabeza pintada de arenisca del rey Amenhotep I (Dinastía XVIII), de 58 cm de alto y 24,5 cm de ancho. El mérito de reunificar Egipto y establecer el Imperio Nuevo corresponde a los dos primeros reyes que gobernaron un Egipto unificado desde el Mediterráneo hasta Nubia: Ahmose I, que expulsó a los hicsos, y Amenhotep I, que consolidó los cimientos de la unidad. Artísticamente, el arte de la Dinastía XVIII temprana imitó y siguió de cerca los estilos del Imperio Medio. Esta cabeza, hallada en Karnak, fue en su día ricamente coloreada: la corona y la barba eran blancas, el ureus amarillo y el rostro rojo. La ausencia de un pilar dorsal sugiere que provenía de una estatua en forma osiriana. Cabezas similares se conservan en los almacenes del Museo de Luxor y en El Cairo, revelando la continuidad artística de las tradiciones del Imperio Medio."
            },

            new()
            {
                Name = "head_of_nakhtmin",
                Description = "La cabeza del comandante militar Nakht-Min, escriba real y comandante de alto rango que sirvió bajo Tutankamón y su sucesor el rey Ay. La cabeza representa a un distinguido joven con rasgos idealizados característicos del estilo post-Amarna de la Dinastía XVIII tardía —retornando gradualmente a las convenciones tradicionales mientras conservaba algunas cualidades naturalistas heredadas del período revolucionario de Akhenatón. Se cree que el rey Horemheb ordenó la destrucción deliberada de esta estatua tras ascender al trono, como parte de una campaña sistemática para borrar la memoria de sus predecesores y establecer su propia legitimidad. Esta destrucción calculada de monumentos era una herramienta política común en el antiguo Egipto."
            },

            new()
            {
                Name = "head_of_sesostris_III",
                Description = "Esta excepcional cabeza de una estatua de Senusret III (Imperio Medio) ha sido aclamada por expertos, desde su descubrimiento el 25 de febrero de 1970, como uno de los descubrimientos modernos más importantes —usada como icono y emblema del Museo de Luxor durante mucho tiempo. Porque representa un cambio radical en la filosofía del arte del antiguo Egipto. Mientras que las estatuas reales glorificaban la divinidad del rey y lo representaban como un ser humano idealizado y perfecto, aquí vemos un llamativo ejemplo de 'Realismo'. Senusret III aparece como un hombre que muestra señales de cansancio y el peso de la responsabilidad: mejillas hundidas, profundas arrugas y líneas de fatiga claramente visibles alrededor de los ojos y la boca. El rey se enfrenta a sus súbditos como un ser humano que lleva las cargas del Estado, no como una deidad intocable. Lleva la Corona Doble (Pschent), con la cobra ureo real. El egiptólogo Georges Legrain había descubierto previamente en el Escondrijo de Karnak en 1904 dos estatuas casi idénticas a esta, ahora en el Museo de El Cairo. Las fotografías comparativas muestran cuán estrechamente coinciden los rasgos, aunque esta cabeza de Luxor fue descubierta más de sesenta años después."
            },

            new()
            {
                Name = "Horemheb_and_his_wife",
                Description = "La doble estatua del general Horemheb y su esposa, tallada en caliza (Dinastía XVIII). Curiosamente, esta estatua fue hallada en su tumba de Saqqara —no en Tebas— la tumba que construyó para sí mismo cuando era comandante del ejército bajo Tutankamón y el rey Ay. Tras ascender al trono, construyó una tumba real en el Valle de los Reyes donde fue sepultado. La estatua se considera una obra maestra del arte post-Amarna; la cabeza de la esposa se perdió en un antiguo robo antes de que la estatua fuera recuperada y colocada en el museo."
            },

            new()
            {
                Name = "inscribed_block_w_for_Akhnaton",
                Description = "Un bloque arquitectónico inscrito que porta cartuchos del rey Akhenatón (Amenhotep IV), datado en el Período de Amarna de la Dinastía XVIII (c. 1365–1347 a.C.). Este bloque fue extraído del relleno de uno de los masivos pilonos de Karnak, donde miles de bloques talatat de los templos de Atón de Akhenatón se utilizaron como relleno de construcción por los reyes posteriores que buscaban borrar toda evidencia de su revolución religiosa. Los cartuchos muestran los nombres de Akhenatón en su forma revisada —adoptada después de que cambiara su identidad y el nombre de su dios de Amón a Atón. Estos bloques talatat estandarizados (aproximadamente 52 × 26 × 24 cm, su longitud equivalente a tres palmos de mano) forman parte del vasto corpus que los académicos egipcios e internacionales han estado reconstruyendo meticulosamente desde los años 1960."
            },

            new()
            {
                Name = "inscribed_block_with_akhnaton_names",
                Description = "Un bloque arquitectónico inscrito que porta cartuchos con los dos nombres reales del rey Akhenatón —su nombre de nacimiento Amenhotep IV y su revolucionario nombre adoptado Akhenatón ('Espíritu Eficaz de Atón'). Datado en el Período de Amarna de la Dinastía XVIII (c. 1365–1347 a.C.), este bloque talatat fue hallado en el relleno de los pilonos de Karnak, donde los templos desmantelados de Atón de Akhenatón fueron utilizados como relleno de escombros por los gobernantes posteriores. El afortunado entierro de estos bloques dentro de sólidos pilonos de piedra preservó sus vívidos colores originales, proporcionando información invaluable sobre el arte y la iconografía religiosa del Período de Amarna. Los egiptólogos han ensamblado hasta la fecha aproximadamente 40.000 bloques talatat de Karnak, creando un notable rompecabezas arqueológico que continúa revelando nuevos detalles sobre el período más controvertido y transformador de la historia religiosa del antiguo Egipto."
            },

            new()
            {
                Name = "jamb_of_incense_hatchepsut",
                Description = "Una jamba de diorita (una roca volcánica extremadamente dura) datada en la Dinastía XVIII (c. 1470 a.C.). Hallada dentro de los cimientos del Tercer Pilono de Amenhotep III, donde los bloques reutilizados de edificios más antiguos se colocaban comúnmente como relleno. Esta pieza es la jamba derecha de un vano, y su inscripción indica que pertenecía al 'Almacén de Incienso' de Karnak, construido por la reina Hatshepsut como memorial y acto de devoción a su padre Amón-Ra. La diorita se extraía del Desierto Oriental cerca de Quseir y de partes del Sinaí. Debido a su dureza, los antiguos egipcios también la utilizaban para fabricar los martillos esféricos empleados para cortar granito, como puede observarse en la cantera del Obelisco Inacabado en Asuán."
            },

            new()
            {
                Name = "Limestone stela of Queen Hatshepsut",
                Description = "Un relieve de caliza de la Dinastía XVIII representa a la reina Hatshepsut de pie tras su esposo el rey Tutmosis II, ambos adorando al dios Amón y presentando ofrendas. La reina aparece en su papel de 'Esposa Real', llevando una falda real y sosteniendo el cetro real en su mano derecha. También se expone una rara estela de caliza de Hatshepsut hallada en Karnak, ejecutada en relieve hundido ante Amón. Su rareza radica en que Hatshepsut aparece como mujer —una inusual desviación; en la mayoría de sus monumentos era representada como hombre (con barba ceremonial y falda corta) para legitimar su reinado. La razón por la que aparece femenina aquí es probablemente porque la inscripción incluye su nombre bajo el título 'Amada de Amón, Hatshepsut'. Hatshepsut es sin duda una de las reinas más famosas y poderosas que gobernó Egipto. Tras la muerte de su esposo-hermano Tutmosis II, administró el país sabiamente durante veintidós años, fomentando el comercio y los proyectos de construcción."
            },

            new()
            {
                Name = "limestone_wall_relief_Thutmose III",
                Description = "Del mismo yacimiento (el templo de Tutmosis III en Deir el-Bahari), un relieve realzado representa al rey llevando la corona azul Khepresh, con su barba ceremonial en azul y el ureo real en su frente. Lleva el amplio collar Wesekh y su piel está pintada en el convencional marrón rojizo usado para los hombres. El templo de Tutmosis III en Deir el-Bahari, situado al sur del templo de Hatshepsut en una terraza rocosa más alta, fue una obra maestra arquitectónica —pero fue completamente destruido por un masivo desprendimiento de rocas en 1120 a.C., haciendo que los objetos recuperados de sus escombros sean especialmente significativos. Artísticamente, los rasgos del rey aquí siguen las convenciones idealizadas de la era más que su semejanza personal. Tutmosis III extendió el poder imperial egipcio desde la Cuarta Catarata en Sudán hasta las orillas del Éufrates en el norte de Siria, estableciendo un imperio sin parangón en el mundo antiguo hasta Alejandro Magno."
            },

            new()
            {
                Name = "Minister_Mentuhotep_writer",
                Description = "Esta estatua representa a Mentuhotep en la pose de 'escriba' —una profesión enormemente respetada en el antiguo Egipto, ya que los escribas desempeñaban papeles vitales en los templos y el palacio real. La estatua sin cabeza está tallada en granito negro, de 61,5 cm de altura, y data de la Dinastía XII, concretamente del reinado de Senusret I. Hallada en Karnak en enero de 1970, Mentuhotep fue uno de los hombres de Estado más prominentes de su era, sirviendo como visir bajo Senusret I y continuando en el reinado de Amenemhat II. Notablemente, encargó un gran número de estatuas personales —al menos ocho identificadas: dos en el Museo de Luxor, tres en El Cairo y tres en el Louvre de París. Ambas estatuas sufrieron daños deliberados durante el Período de Amarna, cuando los trabajadores borraron todas las referencias a los dioses Amón, Montu y Maat. En la época ramésida, un sacerdote llamado 'Roma', Tercer Sacerdote de Amón en Karnak, ordenó restaurar las estatuas añadiendo una inscripción que señalaba que lo había hecho 'por veneración y respeto hacia un gran hombre del pasado lejano'. Mentuhotep aparece sentado con las piernas cruzadas, con un rollo de papiro abierto en su regazo."
            },

            new()
            {
                Name = "mummy_of_ahmose",
                Description = "La sala contiene momias reales con historias notables. Momia de Ramsés I —fundador de la Dinastía XIX: Vendida en 1900 a un museo en las Cataratas del Niágara, transferida posteriormente al Museo Michael C. Carlos en Atlanta, donde pruebas científicas confirmaron su identidad real. El museo la donó posteriormente al pueblo egipcio en reconocimiento a su historia. Momia del rey Ahmose I —conquistador de los hicsos y fundador de la Dinastía XVIII: Hallada en el escondrijo de Deir el-Bahari. Los exámenes indican que este gran héroe murió alrededor de los treinta y tres años y era de constitución delgada, sufriendo de dolor articular. Este contraste confirma que la grandeza de Ahmose no residía en la fuerza física sino en el genio militar y la estrategia sobresaliente que le permitió liderar ejércitos y finalmente liberar a Egipto de la ocupación hicsa."
            },

            new()
            {
                Name = "mummy_of_pharone",
                Description = "Esta momia real, expuesta junto a la momia del rey Ahmose I, data del período ramésida tardío del Imperio Nuevo (Dinastías XIX–XX). El cuerpo está cuidadosamente envuelto en vendas de lino con una máscara de cartón dorada que cubre el rostro, y las manos cruzadas sostienen el cayado y el mayal —los eternos símbolos de la autoridad faraónica. La calidad del proceso de momificación y la presencia de amuletos protectores —incluyendo un escarabeo del corazón, un amuleto djed y un Ojo de Horus— indican que perteneció a un gobernante de considerable estatus. La momia fue descubierta en el famoso escondrijo de Deir el-Bahari (TT320), uno de los descubrimientos arqueológicos más significativos del siglo XIX, donde los sacerdotes de la Dinastía XXI reunieron y volvieron a enterrar secretamente las momias profanadas de los más grandes faraones de Egipto para protegerlos de los saqueadores de tumbas."
            },

            new()
            {
                Name = "obelisk_of_rameses_III",
                Description = "Un obelisco de granito rojo de Ramsés III, de 95,5 cm de altura, hallado en 1923 en el lado occidental del patio entre el Noveno y el Décimo Pilono en Karnak. Sus cuatro caras talladas portan textos jeroglíficos con los nombres y títulos del rey. Los obeliscos eran monumentos solares por excelencia —delgadas agujas de piedra con puntas recubiertas de oro o electro que captaban los primeros rayos del sol de la mañana, sirviendo como símbolos terrestres del montículo primordial de la creación del dios Ra. Lo que hace a este obelisco particularmente notable es que Ramsés III proporcionó tres versiones diferentes de su nombre de Horus —un fenómeno único en la historia faraónica que ningún egiptólogo ha explicado satisfactoriamente hasta ahora."
            },

            new()
            {
                Name = "official_wearing_the_gold_of_honor",
                Description = "Esta estatua, registrada oficialmente como el 'Portador del Oro del Honor', representa a un funcionario que lleva la Decoración del Valor. Fabricada en arenisca, data del Imperio Nuevo, Dinastía XVIII, entre aproximadamente 1440 y 1400 a.C. Fue hallada en la zona de Qaw el-Kebir en el Gobernato de Asyut, adquirida por el Servicio de Antigüedades en 1896. La estatua mide 87 cm de altura. La estatua no tiene inscripciones —una característica distintiva de las estatuas realizadas durante los reinados del rey Amenhotep II y su hijo el rey Tutmosis IV. Lo más llamativo es el doble collar enrollado alrededor del cuello, conocido como el 'Oro del Honor', junto con las bandas y brazaletes dorados que adornan ambos brazos. El rey otorgaba el 'Oro del Honor' en una magnífica ceremonia a los cortesanos que mostraban un valor excepcional en las campañas militares. El escultor se basó en gran medida en modelos artísticos clásicos del Imperio Antiguo —tan marcadamente que un observador primerizo podría pensar que la pieza pertenece a esa era."
            },

            new()
            {
                Name = "Osiris_statue_of_King_Mentuhotep_III",
                Description = "El primer objeto que se encuentra cerca de la columna cilíndrica en la parte superior de la rampa es esta estatua que representa al rey Mentuhotep III, uno de los gobernantes de la Dinastía XI (Imperio Medio), mostrado en la postura 'osiriana'. Datada en aproximadamente el siglo XX a.C., está tallada en arenisca, de 189 cm de alto y 48 cm de ancho. Hallada en el Templo de Montu en Armant en 1951, es uno de los primeros ejemplos de un rey representado con la apariencia del dios de los muertos Osiris —llevando la Corona Blanca (corona del Alto Egipto), una barba ceremonial, y su cuerpo envuelto en una ajustada prenda similar a un sudario. La forma osiriana proliferó en el Imperio Medio como consecuencia natural de la evolución religiosa que siguió al colapso de la Dinastía VI: tras las crisis económicas que habían sacudido la fe del pueblo en la monarquía divina, el culto de Osiris floreció, y cada individuo ganó el derecho a convertirse en un 'Osiris' tras la muerte —sus acciones pesadas en un tribunal divino— abriendo el camino a la vida eterna independientemente de la riqueza. La estatua no tiene inscripciones, así que los egiptólogos recurrieron al estilo artístico para identificarla: el rostro ancho, los ojos estrechos y la leve 'sonrisa arcaica' son rasgos de los reyes de las Dinastías XI y XII tardía."
            },

            new()
            {
                Name = "paser_and_henut",
                Description = "Una estatua de caliza de 'Nebra' (Dinastía XIX), hallada en la zona de 'Zawiyet Umm el-Rakham' en Marsa Matrouh. Nebra lleva una larga túnica plisada y sandalias, con el pie izquierdo adelantado. En su mano derecha sostiene un bastón rematado con la cabeza de la diosa Sekhmet (diosa de la guerra), mientras su mano izquierda reposa sobre su falda en postura de adoración. Ramsés II construyó una cadena de fortalezas para proteger las fronteras occidentales de las incursiones libias, incluyendo la fortaleza de Umm el-Rakham, de la que Nebra fue nombrado comandante y gobernador regional. También se expone una doble estatua de 'Baser y su esposa Hinout' en granito gris, hallada en la fortaleza de 'Tell el-Hebua' en el Sinaí —el primer punto de control en el antiguo camino militar 'Camino de Horus'. Baser aparece como Jefe de Arqueros; la pareja entrelaza sus brazos en un gesto de afecto. La cabeza del comandante militar Nakht-Min —escriba real y comandante del ejército bajo Tutankamón y Ay— representa a un joven de rango distinguido, cuya estatua se cree fue destruida por orden de Horemheb para borrar la memoria de sus predecesores."
            },

            new()
            {
                Name = "pillar_of_sesotris_I",
                Description = "Un pilar osiriano del rey Senusret I (Dinastía XII), tallado en caliza pintada, de 158 cm de altura. Hallado por la misión franco-egipcia al oeste del Primer Pilono en Karnak en 1971. En la Dinastía XII temprana, el templo de Amón no era tan extenso como lo es hoy; consistía en un número limitado de capillas y salas columnadas. Esta gran estatua originalmente se apoyaba contra una de las columnas cuadradas de esas salas, de las que ahora no queda nada. Una estatua prácticamente idéntica se conserva en el Museo de El Cairo, ambas datadas en el reinado de Senusret I (1971–1926 a.C.). El rey aparece en la postura osiriana: la corona fue tallada en una pieza de piedra separada; los ojos son completamente redondeados, dando la impresión de ojos naturales. Los trazos de color revelan claramente el magnífico aspecto original de la estatua: la Corona Blanca y la prenda de lino estaban pintadas en blanco; la piel en rojo; y quedan trazos de azul en la barba ceremonial y los dos signos anj que el rey aprieta en sus brazos cruzados. Imaginar esta estatua en todo su colorido original transmite cuán magníficamente bella debió ser, encarnando la grandeza de los reyes del Imperio Medio."
            },

            new()
            {
                Name = "ramses_II_in_the_double_crown",
                Description = "Estatua de Ramsés II llevando la Corona Doble sobre el tocado nemes —un estilo que él mismo inventó— tallada en una rara combinación de granito negro y rosa. Ramsés II ostenta el título de 'Rey de la Guerra y la Paz' por su amplia fama militar, su largo reinado de 67 años, y porque redactó el primer tratado de paz escrito de la historia (con los hititas)."
            },

            new()
            {
                Name = "relief_of_vectory",
                Description = "Presentamos aquí un artefacto conocido como el relieve de la 'Celebración de la Victoria' —un bloque de arenisca coloreada datado en el Imperio Nuevo, Dinastía XVIII, hallado en el complejo del templo de Karnak. Este relieve se cree que data del reinado del rey Tutankamón, o a lo sumo de su sucesor el rey Ay. Representa un magnífico desfile militar encabezado por portadores de estandartes y banderas, seguidos por filas de soldados armados con escudos, lanzas y espadas. El relieve contiene un importante texto histórico, traducido como: 'Oh Gobernante, eres como el dios Montu en medio del ejército. Que los dioses protejan tus miembros, tras haber eliminado la corrupción que se extendió por la vil tierra de Kush.' La formulación de este texto, con su explícita alabanza militar y comparación directa con el dios de la guerra Montu, lleva a la conclusión de que este relieve probablemente pertenece al rey Ay, que tenía un fuerte trasfondo militar como comandante del ejército antes de asumir el trono."
            },

            new()
            {
                Name = "royal_bows",
                Description = "Una colección de flechas fabricadas en madera, caña, plumas, bronce y hueso, recuperadas de la tumba del rey Tutankamón. Los egipcios estaban entre los arqueros más tempranos de la historia, dominando el arte de disparar con gran precisión desde carros en movimiento —uno de los artes militares más difíciles. Solo en la tumba de Tutankamón se hallaron 278 flechas: algunas muy cortas (menos de 30 cm), probablemente de su infancia, y la mayoría destinadas a la caza y la guerra. El arco simple se usaba antes del Imperio Nuevo, consistente en una sola vara de madera. El arco compuesto apareció tras la invasión hicsa, fabricado con múltiples capas de madera y materiales reforzantes, otorgándole mucha mayor potencia y alcance. Las puntas de flecha se fabricaban de piedra, metal y marfil; por ejemplo, las cabezas de madera en forma de pera se usaban en la caza deportiva para aturdir a la presa sin matarla."
            },

            new()
            {
                Name = "sarcophaus",
                Description = "Imperio Antiguo: Un masivo cofre rectangular de granito o alabastro decorado para asemejarse a una fachada de palacio, con ojos pintados para que el difunto pudiera ver el mundo exterior. Imperio Medio: Aparecieron los 'Textos de los Ataúdes', escritos en el interior para guiar a los muertos, y los ataúdes comenzaron a tomar forma antropoide en madera o piedra. Ataúdes 'Rishi' (emplumados) de la Dinastía XVII: Nombrados por las alas de Isis y Neftis que envuelven al difunto en protección. Imperio Nuevo: Creciente complejidad con juegos de ataúdes anidados (como en el de Tutankamón); fabricados de oro o plata para los reyes, y de madera local para los comunes. Se expone un ataúd de madera pintado en negro (color del renacimiento y la muerte), con inscripciones religiosas del Libro de los Muertos. También se expone un estuche de momia de cartón pintado para la señora Shep-ankh-Khonsu (Período Intermedio Tardío, Dinastías XXI–XXIII), hallado en el área de Asasif. El estuche está decorado con ricas escenas mitológicas: bajo el rostro, un pájaro fantástico con cabeza de carnero (el dios Atum, el Creador), extendiendo sus alas en protección; en el pecho, Osiris en forma momiforme rodeado por los cuatro Hijos de Horus; en el centro, un halcón extendiendo sus alas sobre el emblema de Abidos; y en la parte inferior, dos chacales que representan al dios Wepwawet (Abridor de Caminos)."
            },

            new()
            {
                Name = "Sed_Festival_Sandstone",
                Description = "Un bloque de arenisca que representa escenas del festival Heb-Sed (Jubileo) —una de las ceremonias reales más antiguas e importantes del antiguo Egipto. El Heb-Sed se celebraba normalmente tras treinta años de reinado y luego a intervalos más cortos, sirviendo para renovar los poderes físicos y espirituales del rey y reafirmar su derecho divino a gobernar. La ceremonia implicaba elaborados rituales: el rey corriendo un recorrido simbólico entre marcadores de frontera para demostrar su continua vitalidad física, recibiendo coronas renovadas de los dioses del Alto y Bajo Egipto, y realizando ritos sagrados ante las estatuas de las deidades. El rey Amenhotep III estaba particularmente dedicado a estas antiguas tradiciones, encargando a altos funcionarios buscar en las bibliotecas de los templos documentos antiguos para asegurarse de que sus rituales Heb-Sed se conformaran con precisión a miles de años de precedente."
            },

            new()
            {
                Name = "sekhmet_A",
                Description = "Presentamos aquí estatuas de la diosa Sekhmet, siempre representada con cuerpo de mujer y cabeza de leona. Sekhmet era una de las más importantes diosas de la guerra en el antiguo Egipto —ella quien enviaba muerte y destrucción sobre los enemigos del dios sol Ra. Al mismo tiempo, era conocida por su extraordinario poder sanador, por lo que sus pequeñas estatuas se usaban como amuletos para proteger a los enfermos. Estas estatuas datan del Imperio Nuevo (Dinastía XVIII), concretamente del reinado del rey Amenhotep III (c. 1404-1365 a.C.). Fabricadas en granito gris, esta categoría se distingue por la presencia del disco solar que corona la cabeza de la diosa."
            },

            new()
            {
                Name = "sekhmet_B",
                Description = "El nombre 'Sekhmet' significa 'la Poderosa' en antiguo egipcio. Su principal centro de culto era Menfis (la moderna Mit Rahina), donde era considerada la esposa del dios Ptah y madre del dios Nefertem (la Tríada Menfita). En las creencias del antiguo Egipto, Sekhmet representaba el aspecto colérico del 'Ojo de Ra' —la fuerza destructiva contra los enemigos del dios sol y los enemigos del rey. Su papel es más prominente en el 'Mito de la Destrucción de la Humanidad', donde desempeñó el papel de mensajera de la retribución de Ra, enviada a aniquilar a los seres humanos rebeldes."
            },

            new()
            {
                Name = "sekhmet_c",
                Description = "Durante el Imperio Nuevo, concretamente en el reinado del rey Amenhotep III, se erigieron más de 500 estatuas de la diosa Sekhmet. La mayoría fueron halladas en la zona de Qurna y en el Templo de Mut en Karnak, lo que indica que la diosa Mut era considerada una manifestación o encarnación moderna de la diosa de la guerra Sekhmet. Esta categoría presenta una estatua de Sekhmet sin el disco solar, fabricada en granito gris oscuro. La estatua mide 97,5 cm de altura, 49 cm de anchura y 41 cm de profundidad, con la columna de soporte dorsal de 25,5 cm de anchura."
            },

            new()
            {
                Name = "set_of_cosmatic_tools",
                Description = "Una mesa de ofrendas de esquisto verde (41×47 cm, 6,5 cm de altura) del Período Ptolemaico (300–30 a.C.), hallada por la misión franco-egipcia en 1970 al este del Lago Sagrado en Karnak. Las mesas de ofrendas ptolemaicas se distinguen por las ofrendas talladas en bajorrelieve sobre un fondo rugoso: una estera de papiro, varios tipos de pan, recipientes de purificación y un pato preparado para el sacrificio. La inscripción señala que el propietario se llamaba 'Padi-nefr-hetep' y la dedicó a Osiris. Una segunda mesa de ofrendas en granito gris data de la Dinastía XXX o del período ptolemaico temprano (c. 280–250 a.C.). Parece haber sido un regalo al templo de Amón de un funcionario anónimo que deseaba asegurar ofrendas diarias a los dioses sin necesidad de visitar el templo personalmente —ya que se creía que las imágenes talladas se 'transformarían mágicamente' en comida y bebida reales para el dios. Las ofrendas (pan, flores de loto, pepino) están representadas estrechamente agrupadas sin espaciado, un rasgo estilístico de ese período."
            },

            new()
            {
                Name = "sobek_and_amenhotep III",
                Description = "La pieza central de la sala —y el objeto que capta inmediatamente la mirada al entrar— es esta doble estatua tallada de un solo bloque de alabastro (mármol egipcio). Es la estatua de alabastro más grande del mundo entero. Descubrimiento: hallada el 27 de julio de 1967 durante trabajos de construcción en un canal de irrigación cerca de la ciudad de Armant —apenas 52 días después de la derrota de junio de 1967, un detalle que habla de la resiliencia del Estado egipcio. La estatua fue descubierta en el fondo de un pozo sellado por una losa de arenisca, aparentemente oculta deliberadamente para protegerla del vandalismo. Identificación: inicialmente atribuida a Ramsés II por sus cartuchos. Sin embargo, el cuidadoso examen científico reveló que el propietario original era Amenhotep III —se detectaron trazas de inscripciones borradas, y crucialmente una 'huella dactilar' artística característica de las estatuas de Amenhotep III: una línea tallada entre la ceja y el ojo. Descripción artística: el dios Sobek (en forma de cocodrilo con cabeza humana) extiende su mano para ofrecer el signo anj (vida) al rey Amenhotep III, simbolizando la continuidad de la legitimidad del rey a través del favor divino. Sobek era considerado una poderosa deidad del más allá, pues se sumerge en las profundidades del agua (el inframundo) y emerge de nuevo, vinculándolo a Osiris. Su culto floreció en Kom Ombo y en el Fayum, y en la Dinastía XIII su veneración fue tan intensa que ocho reyes llevaron el nombre 'Sobek-hotep' (Sobek está complacido)."
            },

            new()
            {
                Name = "sphinx",
                Description = "Una esfinge única en pose de ofrenda, tallada en calcita (alabastro egipcio), de 95 cm de largo y 53 cm de alto. Data de finales de la Dinastía XVIII, concretamente del reinado de Tutankamón (1347–1336 a.C.), y fue hallada en el lado occidental del Primer Patio del Templo de Karnak. La esfinge en el antiguo Egipto adoptaba diversas formas, pero su apariencia fundamental era un cuerpo de león con cabeza humana, representando a la deidad Ra-Horajty. Esta esfinge lleva el tocado nemes y una barba ceremonial. Al estudiar los rasgos faciales —en particular la boca delgada y los labios carnosos— los eruditos sugieren que fue tallada en el período de transición entre el reinado de Akhenatón y el comienzo del de Tutankamón. En esta estatua, las extremidades delanteras son brazos humanos que sujetan un recipiente de ofrendas. Esta forma de esfinge con brazos humanos apareció por primera vez bajo Ahmose I y continuó evolucionando hasta el período romano; tales estatuas se colocaban en grandes templos como el de Karnak como símbolo de la devoción del rey y su presentación de ofrendas a los dioses."
            },

            new()
            {
                Name = "state_of_amenhotep_II",
                Description = "Una estatua de granito rosa de Amenhotep II, hallada en Karnak en 1951. El rey lleva la Corona Doble, la barba ceremonial y el ureo real. Cuando se descubrió su parte superior, los eruditos pensaron inicialmente que era su padre Tutmosis III por la similitud de rasgos, pero la inscripción en el pilar dorsal confirmó su identidad. El ejército profesional del Imperio Nuevo comprendía cuatro grandes divisiones, cada una nombrada por un dios (Amón, Ra, Ptah y Set). Cada división constaba de 5.000 combatientes divididos en 20 compañías, cada compañía compuesta por 5 grupos de 50 hombres dirigidos por un oficial. El ejército combinaba jóvenes conscriptos, veteranos y mercenarios extranjeros, una composición que garantizó la seguridad nacional durante siglos."
            },

            new()
            {
                Name = "state_of_amenhotep_III_G",
                Description = "Estatua del rey Amenhotep III en granito gris (143 cm de alto), de su era dorada (1404–1365 a.C.), hallada en 1950 durante excavaciones al norte del Primer Pilono. Amenhotep III es una de las personalidades más fascinantes y complejas de la historia egipcia: comenzó su reinado como un enérgico joven deportista —los textos lo registran como un hábil cazador que mató 102 leones, y un poderoso guerrero que arrasó Nubia. Pero en la segunda mitad de su reinado se produjo una gran transformación; abandonó las actividades atléticas y se sumergió en el lujo y el placer sensual. Curiosamente, Amenhotep III estaba profundamente dedicado a la tradición; encargó a sus altos funcionarios buscar en los documentos antiguos conservados en las bibliotecas de los templos para asegurarse de que los rituales 'Heb Sed' (Jubileo) se conformaran con precisión con las reglas tradicionales heredadas de miles de años —un nivel de exactitud histórica sin precedente para ningún rey anterior. Esta estatua refleja la preferencia del rey por las tradiciones artísticas del Imperio Antiguo y el Imperio Medio; aparece sentado en un trono con un panel dorsal, la mano derecha plana sobre su muslo mientras que la izquierda, cerrada alrededor de un objeto, descansa sobre el muslo izquierdo."
            },

            new()
            {
                Name = "state_of_Amenhotep_IV",
                Description = "Una estatua del rey Akhenatón (Amenhotep IV) llevando la Corona Doble y sosteniendo el cayado y el mayal reales, descubierta en su templo de Atón construido al este del complejo de Karnak. El estilo artístico de Amarna plenamente realizado es inconfundible: el rostro del rey exhibe rasgos sin precedente y profundamente inusuales —un rostro excesivamente alargado, una estrecha frente huidiza, ojos estrechos contemplativos, una nariz larga con profundos surcos nasogenianos, labios carnosos y sensuales, y una mandíbula anormalmente prominente. Este vocabulario artístico revolucionario, que Akhenatón supervisó personalmente, era completamente inédito en miles de años de arte egipcio. En lugar de representar distorsiones, estos rasgos expresaban una nueva teología —devoción absoluta al poder del sol concentrado en el disco de Atón. Tras la muerte de Akhenatón, sus sucesores borraron sistemáticamente esta revolución artística y religiosa, reclamando los dioses tradicionales y las convenciones artísticas."
            },

            new()
            {
                Name = "state_of_god_amon",
                Description = "Esta estatua representa a Amón-Ra, Rey de los Dioses, tallada en caliza. Data del Imperio Nuevo, concretamente del Templo de Karnak durante el reinado de Tutankamón (c. 1347–1336 a.C.). La estatua mide 155 cm de altura y fue descubierta en 1904 en el Escondrijo de Karnak. El rasgo más llamativo es que el rostro del dios porta la semejanza del propio Tutankamón. El joven rey, al regresar a Tebas tras el período de Amarna durante el cual los templos de Amón habían estado cerrados, claramente deseaba demostrar su devoción absoluta a Amón y restaurar el prestigio del dios que su padre Akhenatón había socavado. Amón aparece con su atuendo tradicional: el amplio collar 'Wesekh', y sobre su cabeza la corona tradicional de dos plumas. Notablemente, Amón no surgió como el dios principal de Tebas hasta el comienzo del Imperio Nuevo; la deidad más antigua de la ciudad era 'Montu'. El dios sostiene en ambas manos el 'Nudo de Isis' (Tyet), un talismán que se creía confería vida y protección a quien lo portaba."
            },

            new()
            {
                Name = "state_of_rameses_III",
                Description = "Esta es una estatua del rey Ramsés III, fabricada en esquisto, perteneciente a la Dinastía XX del Imperio Nuevo, descubierta en el templo de Karnak. La estatua representa al rey Ramsés III en una pose devocional, llevando una peluca corta coronada por la doble corona, de pie ante el dios Osiris. Al lado de Ramsés, tras su pierna izquierda, se encuentra una estatua del príncipe y comandante del ejército. Esta estatua fue descubierta en 1930 por la expedición del Instituto Oriental de Chicago. Sin embargo, otra parte fue descubierta en 2002 por una expedición de la Universidad Johns Hopkins bajo el suelo del Templo de la Diosa Mut en Karnak, y todas las piezas fueron ensambladas y completamente restauradas en 2003. El rey Ramsés III es considerado el último de los grandes faraones de la historia egipcia. Rechazó con éxito dos invasiones desde el oeste (desde Libia) y una tercera lanzada por los 'Pueblos del Mar'. A pesar del deterioro de la situación económica de su era, Ramsés III otorgó al templo de Karnak una vasta riqueza. Su época fue testigo también de la primera huelga laboral registrada en la historia. Finalmente, la esposa del rey, Tiy, conspiró con varios cortesanos, tramando el asesinato del rey Ramsés III."
            },

            new()
            {
                Name = "state_of_thai",
                Description = "Aquí también tenemos la estatua del Escriba Real y Jefe de Arqueros, 'Thay'. Fabricada en madera de ébano, data del Imperio Nuevo, Dinastía XVIII. Esta estatua fue hallada en Saqqara, envuelta en vendas de lino y cubierta con una fina capa de yeso, haciéndola parecer a primera vista tallada en caliza. Thay aparece de pie, llevando su túnica y falda plisada. Tallada en el frente de la falda y en la superficie superior de la base se encuentra una fórmula de ofrenda de presentación, junto con el nombre y los títulos de Thay —quien ocupó los cargos de Escriba Real y Jefe de Cuadras de Caballos durante el reinado del rey Amenhotep II. También aparece llevando una peluca y un amplio collar que consiste en cuatro hileras de anillos de oro. La precisión del tallado y la habilidad del artista del antiguo Egipto son claramente evidentes en los finos detalles y rasgos del rostro de Thay."
            },

            new()
            {
                Name = "state_of_thutmusis_III",
                Description = "Esta estatua, a la derecha al entrar al museo, se erige como la contraparte de la estatua de Amenhotep III a la izquierda: mientras esta última representa la 'era de la prosperidad de Tebas', esta representa la 'era del triunfo de Tebas' y la supremacía militar. Es una de las obras maestras del arte del Imperio Nuevo y también proviene del Escondrijo de Karnak, descubierta al norte del Séptimo Pilono en 1904. La estatua mide 90,5 cm de altura, tallada en esquisto verde. En la creencia del antiguo Egipto, el rey era la encarnación terrenal del poder divino que mantenía el orden cósmico (Maat); de ahí que tuviera que ser representado en un estado de perfección física. Tutmosis III aparece como un joven fuerte y apuesto con una sonrisa tranquila y confiada. Avanza con el pie izquierdo adelantado, llevando el tocado real nemes y la falda. Las cejas que se curvan hacia abajo son un rasgo estilístico que apareció en el reinado de Hatshepsut, lo que sugiere que esta estatua fue tallada en los primeros años del reinado de Tutmosis cuando corregía con ella. El sello distintivo de esta obra es su superficie notablemente suave y pulida, a pesar de la dureza de la arenisca verde y la simplicidad de las herramientas disponibles para el escultor egipcio en aquel tiempo."
            },

            new()
            {
                Name = "statue_of_Amenhotep_son_of_Hapu",
                Description = "Presentamos aquí una estatua de uno de los arquitectos y sabios más importantes y grandes del Imperio Nuevo: Amenhotep hijo de Hapu. Fabricada en granito negro, data de la Dinastía XVIII, concretamente del reinado del rey Amenhotep III (c. 1403-1365 a.C.). Descubierta en 1913 en el Templo de Amón en Karnak, en el lado oriental de la fachada norte del Décimo Pilono. La estatua mide 130,5 cm de altura, 79 cm de anchura y 72,5 cm de profundidad. La estatua representa a Amenhotep hijo de Hapu en la pose de un escriba sentado con las piernas cruzadas, contemplando un papiro desenrollado en su regazo. La escritura en el papiro está casi completamente desgastada porque sucesivas generaciones de peregrinos pasaban sus manos por él buscando bendición y conocimiento, reflejando cuánto era venerado en su tiempo. Fue Director de Obras y Supervisor de todos los proyectos del rey, y el rey Amenhotep III le otorgó privilegios casi exclusivamente reservados a los miembros de la familia real. Los textos registran que esta estatua fue encargada como 'intercesora' o 'mediadora' para los adoradores dentro del recinto sagrado del templo de Amón. Este excepcional hombre vivió más de 80 años, y se cree que supervisó la talla y el transporte de los famosos 'Colosos de Memnón' en la Orilla Occidental de Luxor."
            },

            new()
            {
                Name = "stela_of_kamose",
                Description = "Una importante estela histórica de caliza de 231 cm de altura, datada en el reinado de Kamose (finales de la Dinastía XVII), descubierta en 1954 en Karnak entre los cimientos de una estatua del rey Panedjem. Esta estela documenta una fase decisiva de la guerra de liberación contra los hicsos: su texto de 38 líneas describe las victorias militares de Kamose en la capital hicsa Avaris. Su mayor valor histórico reside en su relato de una carta interceptada: el ejército de Kamose interceptó una carta enviada por Apophis, rey de los hicsos, al rey de Kush solicitando asistencia militar y pidiéndole que atacara Egipto desde el sur para aliviar la presión desde el norte. Gracias a la vigilancia de la inteligencia militar egipcia en aquel tiempo, este complot —que podría haber cambiado el curso de la guerra— fue frustrado. La estela concluye con una descripción del regreso triunfal de Kamose a Tebas, mencionando en la esquina inferior izquierda a un hombre llamado 'Nishi' —Supervisor de la Corte y Jefe del Tesoro— a quien el rey encargó erigir esta estela como testigo eterno de la gloria egipcia."
            },

            new()
            {
                Name = "stela_of_Queen_Hatshepsut",
                Description = "Una rara estela de caliza de la reina Hatshepsut, ejecutada en relieve hundido, que la muestra en una notable desviación de sus representaciones habituales. En la gran mayoría de sus monumentos oficiales, Hatshepsut se representaba a sí misma como hombre —llevando la barba ceremonial, la falda real corta y los cinco títulos faraónicos— para legitimar su reinado sin precedente como faraón femenina. En esta estela, sin embargo, aparece en su forma femenina ante el dios Amón, ofreciendo los regalos tradicionales. La presencia de su nombre bajo el título femenino 'Amada de Amón, Hatshepsut' confirma su identidad. Esta estela fue hallada en Karnak y proporciona un fascinante atisbo de la manera compleja y políticamente sofisticada en que Hatshepsut navegó por las exigencias de la ideología real. Bajo su reinado, el comercio egipcio floreció, la famosa flota comercial navegó a la Tierra de Punt, y los proyectos de construcción transformaron Karnak y Deir el-Bahari."
            },

            new()
            {
                Name = "stela_of_rameses_III",
                Description = "Una estela de arenisca de la Dinastía XX, hallada en la aldea de el-Qurna. Representa a Ramsés III de pie ante el dios Amón, quemando incienso en un acto de devoción. Ramsés III es considerado con razón el último de los grandes reyes del antiguo Egipto. A pesar del lapso de tiempo que lo separa de su famoso predecesor Ramsés II, se modeló a sí mismo en él en todos los aspectos —desde sus títulos y los nombres de sus hijos hasta el diseño arquitectónico de su templo funerario en Medinet Habu, que es una clara imitación del Ramesseum. Lo que Ramsés III dio a Egipto no es menos significativo que lo que proporcionó Ramsés II: salvó al país de la amenaza de los 'Pueblos del Mar' que ya habían derrocado grandes potencias como el Imperio Hitita, y aseguró las fronteras occidentales de Egipto contra las incursiones libias. A pesar de toda esta grandeza, su fin fue trágico: fue asesinado en la famosa 'Conspiración del Harén', y con su muerte llegó a su fin la era de los grandes reyes."
            },

            new()
            {
                Name = "stela_or_wall_decoration",
                Description = "Vasijas de cerámica de 'Naqada II' (3200–3000 a.C.), un período que precede a la unificación de las Dos Tierras bajo Narmer. La cerámica de 'Gerza' se distingue por su decoración en marrón rojizo sobre un fondo beige, mientras que otro estilo presenta la técnica de 'parte superior negra': la vasija se colocaba boca abajo mientras aún estaba caliente sobre paja ardiendo —un método que aún se practica en Sudán hoy. También se expone un objeto de cuarcita que representa una cabeza de carnero emergiendo de una flor de loto (Dinastía XIX). El carnero es sagrado para Amón (con cuernos curvados) y para Khnum (con cuernos horizontales). Esta pieza decoraba en su día la proa de una pequeña barca sagrada usada en las procesiones del Nilo. Debido al vínculo entre la cabeza de carnero y la flor de loto, se data a partir del comienzo del reinado de Seti I."
            },

            new()
            {
                Name = "temple_wall_of_Amenhotep_IV",
                Description = "Una magnífica pared de 18 metros de largo y 4 metros de alto, reconstruida con éxito por el Centro Franco-Egipcio (CFEETK) en 1968–1969 utilizando bloques talatat extraídos del relleno del Noveno Pilono del Templo de Karnak. Estos bloques datan del reinado de Akhenatón (Dinastía XVIII, c. 1365–1347 a.C.) y representan uno de los mayores tesoros artísticos del Museo de Luxor. Los bloques 'talatat' son bloques de piedra estandarizados de aproximadamente 52 × 26 × 24 cm, cuyo nombre deriva del árabe para 'tres' (thalatha), ya que la longitud de cada bloque equivale a tres palmos de mano. En los primeros cinco años del reinado de Amenhotep IV, ordenó construir templos en honor a su dios favorito Atón en el lado oriental de Karnak. Las paredes de estos templos estaban adornadas con bellísimas escenas de la vida cotidiana en color. Tras el final del período de Amarna, los reyes posteriores demolieron los templos de Atón y utilizaron los bloques como relleno en los cimientos de los pilonos de Amón. Los egiptólogos lograron ensamblar aproximadamente cuarenta mil de estos bloques; su enterramiento dentro de los pilonos había preservado afortunadamente sus colores originales. La reconstrucción de esta pared fue posible cuando los excavadores encontraron los bloques en el Noveno Pilono dispuestos sistemáticamente —permitiendo reconstruir la pared en preciso orden histórico y espacial tras 33 siglos. La pared comprende 283 bloques de piedra. En el lado izquierdo (norte), Akhenatón adora el poder creador del disco solar Atón, cuyos rayos terminan en pequeñas manos humanas que extienden el signo anj (símbolo de la vida) al rey. En el lado derecho, las escenas representan las actividades diarias de los trabajadores vinculados a los almacenes y talleres del templo —incluyendo un granjero alimentando a un ternero, dos gansos recogiendo grano y tres trabajadores apilando hogazas de pan bajo la supervisión de escribas."
            },

            new()
            {
                Name = "thutmusis_III",
                Description = "Una magnífica estatua de Tutmosis III tallada en diorita, hallada en 1965 entre las ruinas de su templo en Deir el-Bahari en condiciones extremadamente precarias; los restauradores de la época la colocaron en un molde de hormigón armado para evitar que se desmoronara, y permaneció en almacenamiento durante 38 años como un 'caso sin esperanza'. En 2003, por decisión del Dr. Zahi Hawass, la tarea de restaurarla fue encomendada al hábil restaurador egipcio Lotfi Khaled Hassan y su equipo. Retiraron cuidadosamente el molde de hormigón, desmontaron la base agrietada, y reensamblaron las piezas utilizando modernas técnicas de inyección y consolidación. Hoy la estatua se alza en toda su majestad y belleza artística como un ícono del nuevo ala del museo."
            },

            new()
            {
                Name = "top_of_niche",
                Description = "Un busto de caliza del padre de 'Amun-mes' (Dinastía XIX), colocado por Amun-mes en su tumba para honrar a su padre Ba-en-gerti. Las estatuas de antepasados proliferaron en el Imperio Nuevo como expresión de orgullo en el linaje egipcio auténtico (Rekhet) en contraste con los extranjeros (Khasut). Estas figuras de busto se colocaban en nichos dentro de los hogares para recibir ofrendas diarias a los antepasados. Finalmente, la estatua de 'Qana-amun' arrodillado sosteniendo la Tríada Tebana (Amón, Mut, Khonsu), y la estatua de 'Babs' (Dinastía XXVI) presentando una estatua de la 'Esposa del Dios de Amón' (Nitiqret). El cargo de 'Esposa del Dios' era un importante oficio político y religioso en los períodos posteriores, en el que la Esposa del Dios era el principal foco del culto de Amón en Tebas con amplia influencia económica."
            },

            new()
            {
                Name = "two_wooden_masks",
                Description = "Este tipo de máscara funeraria comenzó a descubrirse a partir del siglo XVIII, hallado en toda Egipto desde Alejandría hasta Asuán. Los estudios científicos confirman su conexión con la creencia religiosa del antiguo Egipto, especialmente en la era ptolemaica. En el período romano, se produjo un cambio fundamental de estilo: en lugar de seguir estrictas convenciones faraónicas, las máscaras comenzaron a representar los rasgos personales realistas de sus propietarios, influenciadas por las corrientes artísticas romanas. El concepto romano de inmortalidad difería del faraónico: la inmortalidad no se lograba uniéndose al reino de Osiris en el cielo, sino una inmortalidad 'terrenal' —a través de perpetuar las virtudes y la grandeza de una persona como modelo para las generaciones futuras. Estas máscaras de yeso se colocaban a veces en los patios de las casas de las familias de los difuntos. Fabricadas con diversos materiales locales disponibles (yeso, lino, madera), se moldeaban en moldes preparados y se presionaban desde el interior con los dedos, mientras que los detalles finos como orejas, ojos y peinado se añadían a mano. Los ejemplos aquí expuestos pertenecen a un tipo más antiguo del Período Tardío y todavía conservan rasgos claramente egipcios como la barba ceremonial, a diferencia de las máscaras puramente romanas."
            },

            new()
            {
                Name = "votive_state",
                Description = "Una estatua votiva del dios Sobek, en granito negro, de principios de la Dinastía XIX. Hallada en la zona de 'Dahamsha' (templo de Sobek) en 1966 —la misma zona que proporcionó la famosa estatua de Sobek y Amenhotep III en el centro de la sala. Esta es una 'estatua votiva' donada por un funcionario llamado 'Mai' al templo de Sobek. Mai era un funcionario local en la ciudad de 'Sumenu' (la actual aldea de Damsheq). Aparece arrodillado, presentando una estatua de un cocodrilo sobre una base elevada. Lamentablemente, la parte superior ha sido deliberadamente destruida, y dos imágenes en relieve hundido de Mai en el frente también fueron borradas —evidentemente obra de enemigos que deseaban dañar su memoria y obliterar su nombre tras la muerte. La base porta oraciones dirigidas a Sobek por Mai, en las que declara que vivió una vida pura y honrada y que 'pasó sus días nutrido de verdad y justicia'. Artísticamente, Mai lleva la falda plisada 'abultada' —la moda que surgió en la Dinastía XVIII tardía y persistió a través del período de Amarna y más allá. La estatua demuestra una artesanía extraordinaria, con minuciosos detalles como las correas trenzadas de las sandalias de Mai, confirmando que fue realizada en una era de máximo logro artístico."
            },

            new()
            {
                Name = "wallpainting_amenhoteb_III",
                Description = "Presentamos aquí un magnífico relieve pintado en yeso que data del Imperio Nuevo, concretamente del reinado del rey Amenhotep III (c. 1403-1365 a.C.). Hallado en la zona de Sheikh Abd el-Qurna, en la mitad norte de la pared occidental del patio de la Tumba 226 —perteneciente a un alto funcionario— este artefacto fue descubierto en 1913. Aunque grandes partes de esta pintura se han perdido con el tiempo, las secciones restantes son suficientes para confirmar que este panel es una obra maestra vibrante de brillantes colores. Descripción de la escena: el rey Amenhotep III aparece sentado en su trono bajo un elaborado dosel, llevando la 'Corona Azul de Guerra' (conocida como el khepresh), y sosteniendo los símbolos reales del poder. La reina madre Mutemwiya está de pie detrás del rey, colocando su mano sobre su hombro y brazo en un gesto de orgullo maternal. Dos cortesanos reales aparecen inclinándose, abanicando el rostro del rey —uno a cada lado. El título 'Portador de Abanico a la Derecha del Rey' era considerado uno de los cargos más prestigiosos de la corte real del antiguo Egipto. El arte de la pintura alcanzó su cúspide y gloria durante el reinado del rey Amenhotep III. Esta pintura coloreada es el único mural extraído de las tumbas que está preservado y expuesto por el Museo de Arte del Antiguo Egipto de Luxor."
            },

            new()
            {
                Name = "wepones_of_new_kingdom",
                Description = "La vitrina contiene una colección de armas que refleja el desarrollo militar egipcio: hojas de hacha de bronce; un hacha de Tutmosis IV en madera y bronce; una hoja de daga de bronce; puntas de flecha de bronce; mangos de escudo y flechas de madera, algunas pintadas en colores de piel de vaca; un ostracon que representa al dios sirio Resheph (una deidad de la guerra venerada en Egipto); un ostracon que representa a dos soldados en posición de lucha o entrenamiento; y una honda del ajuar funerario de Tutankamón."
            },

            new()
            {
                Name = "amenemhat_III",
                Description = "Un fragmento de estatua en grauvaca que representa al rey Amenemhat III, uno de los gobernantes más poderosos y destacados de la Dinastía XII (c. 1860–1814 a.C.). El rey lleva el tocado nemes con un ureo, y sus rasgos faciales exhiben el característico estilo 'realista' del Imperio Medio tardío —ojos cansados, pómulos prominentes y una expresión seria e introspectiva que se aleja de la perfección idealizada de los retratos reales anteriores. Originalmente situado en su templo funerario en Hawara en el Fayum, esta pieza demuestra el deliberado cambio artístico hacia una representación más humanizada de la realeza, reflejando a un rey agobiado por el peso del gobierno. El reinado de Amenemhat III fue notable por masivos proyectos de construcción incluyendo el legendario complejo del Laberinto en Hawara —un templo mortuorio tan vasto y complejo que los visitantes de la antigüedad, incluido Heródoto, lo describían como más impresionante que las propias pirámides."
            },

            new()
            {
                Name = "amon em Anit",
                Description = "Un relieve realzado pintado que representa al dios Amón-Min, descubierto en 1962 por una expedición polaca que trabajaba en Deir el-Bahari en el templo de Tutmosis III. Ejecutado en caliza pintada, data de la Dinastía XVIII. Durante el Período de Amarna, los templos de Amón fueron sistemáticamente vandalizados. Tras el fin de la revolución de Akhenatón, los reyes posteriores de la Dinastía XVIII y la Dinastía XIX temprana restauraron lo que había sido destruido. Horemheb (1332–1305 a.C.) ordenó retallar y repintar esta escena; sin embargo, los expertos en arte señalan que el artista restaurador no pudo recuperar completamente la vitalidad y precisión características de la obra original de Tutmosis III. Amón-Min (una fusión de Amón, señor de Tebas, y Min, dios de la fertilidad) aparece con el rostro pintado de negro —un color que simboliza la fertilidad y la tierra negra de Egipto que los egipcios llamaban 'Kemet' (la Tierra Negra)."
            },

            new()
            {
                Name = "eh_hotep_necklace",
                Description = "Este colgante o collar es conocido como la 'Mosca Dorada del Valor', perteneciente a la reina Ahhotep. Fabricado en oro puro, data de la Decimoséptima o Decimoctava Dinastía, ya que la reina vivió a través de los reinados de ambas dinastías. Fue descubierto en la zona de Dra Abu el-Naga. El collar consiste en una cadena de la que cuelgan tres colgantes en forma de mosca, llamados las 'Moscas Doradas del Valor'. Este tipo de decoración se otorgaba a los soldados que mostraban un valor excepcional y lograban hazañas sobresalientes en la batalla. Hallado en la tumba de la reina Ahhotep —madre del rey Ahmose, quien coreinaba con él— ella desempeñó un papel prominente e influyente durante la guerra de liberación contra los hicsos, administrando eficazmente los asuntos del Estado egipcio mientras su hijo Ahmose luchaba en los frentes del norte."
            },

        };

        collection.InsertMany(pieces);
        Console.WriteLine($"✅ Seeded {pieces.Count} Spanish piece descriptions.");
    }
}
