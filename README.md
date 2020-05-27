# aim-trainer-2d

# Introduction
Aim Trainer est un projet de jeu Unity 2D programmé en C# jouable à 1 seul joueur.

Le jeu est entièrement basé sur des sprites libres de droits.

Le but du jeu est de détruire le maximum de cibles apparaissant à l’écran en cliquant dessus.

L’objectif est d’améliorer la mémoire musculaire de l’utilisateur avec une souris pour des performances accrues dans les jeux de type FPS. Cela reste tout de même limité car la transition de la 2D à la 3D est difficile.

# Interfaces
Le jeu sera à utiliser en plein écran. On peut observer ci-dessous en gris clair la zone “morte” et une zone en gris foncé qui sera la zone de jeu cliquable.

![img](https://i.gyazo.com/80ef96a1374a2fae8979be87ad7c0dcf.png)

L’écran d’accueil comporte 3 titres.

JOUER pour lancer le jeu

OPTIONS pour modifier la configuration du jeu

QUITTER pour quitter le jeu

![img](https://i.gyazo.com/c0237484a56eab553a5b8b1429850c89.png)

# Choix des couleurs
Le choix des couleurs a été effectué avec le site Paletton.

![img](https://i.gyazo.com/94446934c84eab61b1eedbcdb2a6f02c.png)

La couleur principale sera le rouge (#AA3E39) pour les sprites et les menus. Le fond sera noir.

# Principe & règles
Le principe de ce jeu repose sur l'habileté de l’utilisateur à atteindre plusieurs cibles avec précision en un minimum de temps pour acquérir le maximum de points.

La difficulté du jeu résulte du fait que les cibles apparaissent à des positions x, y aléatoires et une taille aléatoire. Les cibles peuvent être en mouvement ou restent immobiles.

L’utilisateur dispose de 3 vies.

Au début d’une partie, les cibles apparaissent progressivement, avec une seconde d’intervalle. Quand une cible est touché, elle disparaît instantanément.

Plus l’utilisateur progresse dans le jeu, plus le délai d’apparition des cibles diminue.

Lorsque l’utilisateur clique dans le vide alors qu’au moins une cible est présente sur le plateau de jeu, l’utilisateur perd des points de score.

Lorsque l’utilisateur clique sur une cible et la détruit, l’utilisateur gagne des points de score.

Si l’utilisateur détruit au moins 5 cibles sans se manquer (sans cliquer dans le vide), il entre en mode combo et gagne plus de points.

Tant qu’il y a 6 cibles ou moins sur le plateau de jeu, l’utilisateur peut tenter de les détruire en cliquant dessus. Dès que 7 cibles ou plus sont simultanément présentes sur le plateau de jeu, la partie s’arrête et le score de l'utilisateur est sauvegardé.

Si une cible est présente sur le plateau de jeu depuis plus d’une seconde, sa taille diminue progressivement jusqu’à ce qu’elle disparaisse.

Lorsqu’une cible disparaît sans être cliquée, l’utilisateur perd une vie.

Si l’utilisateur réussi un combo de 10 cibles détruites sans se manquer et que l’utilisateur a moins de 3 vie, il gagne une vie.

Si l’utilisateur n’a plus de vie, la partie s’arrête et le score de l’utilisateur est sauvegardé.

# Ressources utilisées

Pour les cibles, un asset gratuit est utilisé : https://assetstore.unity.com/packages/3d/animations/2d-targets-sprites-142142?aid=1101l3b93&utm_source=aff

Il y aura un son lorsque lorsqu’une cible est détruite et lorsque l’utilisateur rate son clic.
