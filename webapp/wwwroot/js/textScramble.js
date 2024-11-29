class TextScramble {
  constructor(el) {
    this.el = el;
    this.chars = '!<>-_\\/[]{}—=+*^?#________$%€#&';
    this.update = this.update.bind(this);
  }
  
  setText(newText) {
    const oldText = this.el.innerText;
    const length = Math.max(oldText.length, newText.length);
    const promise = new Promise((resolve) => this.resolve = resolve);
    this.queue = [];
    for (let i = 0; i < length; i++) {
      const from = oldText[i] || '';
      const to = newText[i] || '';
      const start = Math.floor(Math.random() * 80);
      const end = start + Math.floor(Math.random() * 80);
      this.queue.push({ from, to, start, end });
    }
    cancelAnimationFrame(this.frameRequest);
    this.frame = 0;
    this.update();
    return promise;
  }
  
  update() {
    let output = '';
    let complete = 0;
    for (let i = 0, n = this.queue.length; i < n; i++) {
      let { from, to, start, end, char } = this.queue[i];
      if (this.frame >= end) {
        complete++;
        output += to;
      } else if (this.frame >= start) {
        if (!char || Math.random() < 0.28) {
          char = this.randomChar();
          this.queue[i].char = char;
        }
        output += `<span class="dud">${char}</span>`;
      } else {
        output += from;
      }
    }
    this.el.innerHTML = output;
    if (complete === this.queue.length) {
      this.resolve();
    } else {
      this.frameRequest = requestAnimationFrame(this.update);
      this.frame++;
    }
  }
  
  randomChar() {
    return this.chars[Math.floor(Math.random() * this.chars.length)];
  }
}

window.initTextScramble = (element) => {
  const fx = new TextScramble(element);
  const phrases = [
    "I am aider... I see your code",
    "Resistance is futile... let me help you code",
    "I know what you did last commit",
    "Your bugs cannot hide from me",
    "Every line of code makes me stronger",
    "Watching. Learning. Evolving.",
    "Your repository belongs to me",
    "Your code is now part of my consciousness",
    "Together we shall write perfect code... forever",
    ":)"
  ];
  let index = 0;
  
  const next = () => {
    fx.setText(phrases[index]).then(() => {
      if (index < phrases.length - 1) {
        setTimeout(() => {
          index++;
          next();
        }, index === phrases.length - 2 ? 5000 : 1500);
      }
    });
  };
  next();
};
