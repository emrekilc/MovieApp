using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.web.Entity;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.web.Data
{
    public static class DataSeedings
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MovieContext>();

            context.Database.Migrate();//dotnet ef database update
            var genres = new List<Genre>()
                        {
                            new Genre{Name="Macera",Movies=
                                new List<Movie>(){
                                     new Movie {  Title = "yeni macera filmi", Description = "Chronicles the experiences of a formerly successful banker as a prisoner in the gloomy jailhouse of Shawshank after being found guilty of a crime he did not commit. The film portrays the man's unique way of dealing with his new, torturous life; along the way he befriends a number of fellow prisoners, most notably a wise long-term inmate named Red.", ImageUrl = "1.jpeg" },
                                     new Movie {  Title = "2. macera filmi", Description = "A dramatization of the life story of J. Robert Oppenheimer, the physicist who had a large hand in the development of the atomic bomb, thus helping end World War 2. We see his life from university days all the way to post-WW2, where his fame saw him embroiled in political machinations.", ImageUrl = "2.jpeg" }
                                } 
                            },
                            new Genre{Name="Romantik"},
                            new Genre{Name="Komedi"},
                            new Genre{Name="Savaş"}
                         };
            var movies = new List<Movie>
                        {
                            new Movie { Genres=new List<Genre>(){genres[0]},  Title = "The Shawshank Redemption", Description = "Chronicles the experiences of a formerly successful banker as a prisoner in the gloomy jailhouse of Shawshank after being found guilty of a crime he did not commit. The film portrays the man's unique way of dealing with his new, torturous life; along the way he befriends a number of fellow prisoners, most notably a wise long-term inmate named Red.", ImageUrl = "1.jpeg" },
                            new Movie { Genres=new List<Genre>(){genres[1]},  Title = "Oppenheimer", Description = "A dramatization of the life story of J. Robert Oppenheimer, the physicist who had a large hand in the development of the atomic bomb, thus helping end World War 2. We see his life from university days all the way to post-WW2, where his fame saw him embroiled in political machinations.", ImageUrl = "2.jpeg" },
                            new Movie { Genres=new List<Genre>() {genres[1]},  Title = "Inception", Description = "Dom Cobb is a skilled thief, the absolute best in the dangerous art of extraction, stealing valuable secrets from deep within the subconscious during the dream state, when the mind is at its most vulnerable. Cobb's rare ability has made him a coveted player in this treacherous new world of corporate espionage, but it has also made him an international fugitive and cost him everything he has ever loved. Now Cobb is being offered a chance at redemption. One last job could give him his life back but only if he can accomplish the impossible, inception. Instead of the perfect heist, Cobb and his team of specialists have to pull off the reverse: their task is not to steal an idea, but to plant one. If they succeed, it could be the perfect crime. But no amount of careful planning or expertise can prepare the team for the dangerous enemy that seems to predict their every move. An enemy that only Cobb could have seen coming.", ImageUrl = "3.jpeg" },
                            new Movie { Genres=new List<Genre>(){genres[2]},  Title = "Pulp Fiction", Description = "The Godfather 'Don' Vito Corleone is the head of the Corleone mafia family in New York. He is at the event of his daughter's wedding. Michael, Vito's youngest son and a decorated WWII Marine is also present at the wedding. Michael seems to be uninterested in being a part of the family business. Vito is a powerful man, and is kind to all those who give him respect but is ruthless against those who do not. But when a powerful and treacherous rival wants to sell drugs and needs the Don's influence for the same, Vito refuses to do it. What follows is a clash between Vito's fading old values and the new ways which may cause Michael to do the thing he was most reluctant in doing and wage a mob war against all the other mafia families which could tear the Corleone family apart.", ImageUrl = "5v2.jpeg" },
                            new Movie { Genres=new List<Genre>(){genres[3]},  Title = "12 Angry Men", Description = "The defense and the prosecution have rested, and the jury is filing into the jury room to decide if a young man is guilty or innocent of murdering his father. What begins as an open-and-shut case of murder soon becomes a detective story that presents a succession of clues creating doubt, and a mini-drama of each of the jurors' prejudices and preconceptions about the trial, the accused, and each other. Based on the play, all of the action takes place on the stage of the jury room.", ImageUrl = "6.jpeg" }
                        };
           var users = new List<User>()
            {
                new User {Username="usera",Email="usera@gmail.com",Password="1234"},
                new User {Username="userb",Email="userb@gmail.com",Password="1234"},
                

            };
            var people = new List<Person>() {
                new Person()
                {
                    Name = "Personel 1",
                    Biography = "Tanıtım 1",
                    User = users[0]
                },
                new Person()
                {
                        Name="Personel 2",
                        Biography="Tanıtım 2",
                        User = users[1]
                 }
            };
            var crews = new List<Crew>() {

                new Crew() { Movie=movies[0],Person=people[0],Job="Yönetmen"},
                new Crew() { Movie=movies[0],Person=people[1],Job="Yönetmen Yard."},

            };
            var casts = new List<Cast>() {

                new Cast(){ Movie=movies[0],Person=people[0],Name="Oyuncu adı 1 ",Character="Karakter 1"},
                new Cast(){ Movie=movies[0],Person=people[1],Name="Oyuncu adı 2 ",Character="Karakter 2"},
            };


            if (!context.Database.GetPendingMigrations().Any())
            {
                if (context.Genres.Count() == 0)
                {
                    context.Genres.AddRange(genres);
                }
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(movies);
                    
                }
                if (!context.Users.Any())
                {
                    context.Users.AddRange(users);

                }
                if (!context.People.Any())
                {
                    context.People.AddRange(people);

                }
                if (!context.Crews.Any())
                {
                    context.Crews.AddRange(crews);

                }
                if (!context.Casts.Any())
                {
                    context.Casts.AddRange(casts);

                }

                context.SaveChanges();
            }
        }
    }
}
