import { Movie } from './movie.js';

export class MovieApi {
    private imageUrl = 'https://image.tmdb.org/t/p/original';
    private movieTitle = document.getElementById('movie-title')!;
    private voteAverage = document.getElementById('vote_average')!;
    private popularity = document.getElementById('popularity')!;
    private releaseDate = document.getElementById('release_date')!;
    private description = document.getElementById('movie-description')!;
    private moviesList = document.getElementById('movies-list')!;

    showMoviesList(movies: Movie[], activeIndex: number, onClick: (index: number) => void) {
      const moviesList = document.getElementById('movies-list');
      if (!moviesList) return;
  
      moviesList.innerHTML = '';
  
      movies.forEach((movie, index) => {
          if (!movie.poster_path) return;
  
          const poster = document.createElement('div');
          poster.className = `poster ${index === activeIndex ? 'active' : ''}`;
          poster.onclick = () => onClick(index);
  
          const img = document.createElement('img');
          img.src = `${this.imageUrl}${movie.poster_path}`;
          img.alt = movie.title;
          img.loading = 'lazy';
          
          img.onerror = () => {
              img.src = 'placeholder.jpg';
              img.style.objectFit = 'contain';
          };
  
          poster.appendChild(img);
          moviesList.appendChild(poster);
      });
  }

    showMovieDetails(movie: Movie) {
        this.movieTitle.textContent = movie.title;
        this.voteAverage.textContent = movie.vote_average.toString();
        this.popularity.textContent = `(${movie.popularity})`;
        this.releaseDate.textContent = movie.release_date;

        if (movie.overview.length > 300) {
            const shortText = movie.overview.slice(0, 300);
            this.description.innerHTML = `
                ${shortText}<span id="dots">...</span>
                <span id="more" style="display:none;">${movie.overview.slice(300)}</span>
                <button id="toggle-btn" style="background:none; border:none; color:#F5C51C; cursor:pointer; font-size: 1rem">See More</button>
            `;
            this.setupToggle();
        } else {
            this.description.textContent = movie.overview;
        }

        if (movie.backdrop_path) {
            document.body.style.backgroundImage = `url('${this.imageUrl}${movie.backdrop_path}')`;
            document.body.style.backgroundSize = 'cover';
            document.body.style.backgroundPosition = 'center';
            document.body.style.backgroundRepeat = 'no-repeat';
            document.body.style.backgroundAttachment = 'fixed';
        }
    }

    private setupToggle() {
        const toggleBtn = document.getElementById("toggle-btn");
        const moreText = document.getElementById("more");
        const dots = document.getElementById("dots");

        if (toggleBtn && moreText && dots) {
            toggleBtn.onclick = () => {
                const isHidden = moreText.style.display === "none";
                moreText.style.display = isHidden ? "inline" : "none";
                dots.style.display = isHidden ? "none" : "inline";
                toggleBtn.textContent = isHidden ? "See Less" : "See More";
            };
        }
    }
}